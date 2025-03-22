using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Client.Core.Services;
using Client.UI.Model.BlackList;
using Client.UI.Model.Dungeon;
using Client.UI.Model.PlayerModel;
using Microsoft.Extensions.Logging;
using Wpf.Ui;

namespace Client.Core;

public sealed class MythicStoneClientService :
    IPlayerDataSearchService,
    IStaticResourcesService,
    IBlackListService,
    IDisposable
{
    private readonly HttpClient _cli = new HttpClient();
    private readonly ILogger<MythicStoneClientService> _logger;
    private readonly ProfileService _profile;

    private readonly string _host =
#if DEBUG
        "http://localhost:8080";
#else
            "https://mythicstone.plus";
#endif


    private bool readyToUse = false;

    private string _apiHost { get; }

    public MythicStoneClientService(
        ILogger<MythicStoneClientService> logger,
        ProfileService profile
    )
    {
        _apiHost = $"{_host}/api/v1";
        _profile = profile;
        _logger = logger;


        Initialize();
    }

    public void Dispose() => _cli?.Dispose();


    private void Initialize()
    {
        if (_profile.IsFirstTime())
        {
            _cli.DefaultRequestHeaders.Add("Authorization", "mythicstone.plus.user");
        }

        _cli.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Identification();
    }

    private void WaitToReady()
    {
        while (!readyToUse)
        {
            Thread.Sleep(1000);
        }
    }

    private async void Identification()
    {
        try
        {
            var parameters = new Dictionary<string, string>
            {
                { "uid", _profile.UID ?? "" },
            };  

            var content = new FormUrlEncodedContent(parameters);

            var response = await _cli.PostAsync($"{_host}/client/login",
                content).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<Response<Authorization>>();
            if (data?.Data is not null)
            {
                _profile.UID = data.Data.UID;
                _cli.DefaultRequestHeaders.Remove("Authorization");
                readyToUse = true;
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to identify");
        }
    }


    public async Task<Response<PlayerInfo?>> GetPlayerInfoAsync(string name, string server,
        CancellationToken cancellationToken = default)
    {
        try
        {
            WaitToReady();
            var response = await _cli.GetAsync(
                    $"{_apiHost}/player/info?name={name}&realm={server}", cancellationToken)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Response<PlayerInfo?>>(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get player info");
            throw;
        }
    }

    public async Task<Response<PeriodRating?>> GetRoleThisPeriodScoreAsync(string name, string server,
        CancellationToken cancellationToken = default)
    {
        try
        {
            WaitToReady();
            var response = await _cli.GetAsync(
                    $"{_apiHost}/player/period?name={name}&realm={server}", cancellationToken)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Response<PeriodRating?>>(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "fail to get period score");
            throw;
        }
    }

    public async Task<Response<List<DungeonRating?>>> GetRoleDungeonInfoAsync(string name, string server,
        CancellationToken cancellationToken = default)
    {
        try
        {
            WaitToReady();
            var response = await _cli.GetAsync(
                    $"{_apiHost}/player/dungeon?name={name}&realm={server}", cancellationToken)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Response<List<DungeonRating?>>>(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get Dungeon info");
            throw;
        }
    }

    public async Task<Response<List<DungeonInfo>?>> GetDungeonListAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            WaitToReady();
            var response = await _cli.GetAsync(
                    $"{_apiHost}/dungeon/list", cancellationToken)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Response<List<DungeonInfo>?>>(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get Dungeon info");
            throw;
        }
    }


    public async Task<Response<List<SuspendPlayer>?>> GetUserBlackListAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            WaitToReady();
            var response = await _cli.GetAsync(
                    $"{_apiHost}/blacklist", cancellationToken)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Response<List<SuspendPlayer>?>>();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get black list");
            throw;
        }
    }


    public async Task<Message?> AddUserToBlackListAsync(string name, string realm, string reason,
        CancellationToken cancellationToken = default)
    {
        try
        {
            WaitToReady();
            var parameters = new Dictionary<string, string>
            {
                { "name", name },
                { "realm", realm },
                { "reason", reason },
            };

            var content = new FormUrlEncodedContent(parameters);


            var response = await _cli.PostAsync(
                    $"{_apiHost}/blacklist", content)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Message?>();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get black list");
            throw;
        }
    }

    public async void RemoveUserFromBlackListAsync(string name, string realm,
        CancellationToken cancellationToken = default)
    {
        WaitToReady();
        var response = await _cli.DeleteAsync(
                $"{_apiHost}/blacklist?name={name}&realm={realm}", cancellationToken)
            .ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        // return await response.Content.ReadFromJsonAsync<Response<List<SuspendPlayer>?>>();
    }
}