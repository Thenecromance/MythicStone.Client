using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Client.UI.Model.Dungeon;
using Client.UI.Model.PlayerModel;
using Microsoft.Extensions.Logging;
using Wpf.Ui;

namespace Client.Core;

public sealed class MythicStoneClientService : IClientService
{
    private readonly HttpClient _cli = new HttpClient();
    private readonly ILogger<MythicStoneClientService> _logger;


    private string _host { get; }

    private string _apiHost { get; }


    public MythicStoneClientService(ILogger<MythicStoneClientService> logger)
    {
        _host =
#if DEBUG
            "http://localhost:8080";
#else
            "https://mythicstone.plus";
#endif
        _apiHost = $"{_host}/api/v1";


        _logger = logger;
        _cli.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        Identification();
    }

    public void Dispose() => _cli?.Dispose();


    private async void Identification()
    {
        try
        {
            var parameters = new Dictionary<string, string>
            {
                { "uid", "3823e973-f729-4d60-959d-676d581c7eaa" }
            };
            var content = new FormUrlEncodedContent(parameters);
            var response = await _cli.PostAsync($"{_host}/client/login",
                content).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var data = await response.Content.ReadFromJsonAsync<Response<Authorization>>();
            if (data?.Data is not null)
            {
                _cli.DefaultRequestHeaders.Remove("Authorization");
                _cli.DefaultRequestHeaders.Add("Authorization", data.Data.Token);
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

    public void GetPlayerBestScoreAsync(string name, string server, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void GetRoleBestScoreAsync(string name, string server, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void GetRoleBestScoreDetailAsync(string name, string server, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<PeriodRating?>> GetRoleThisPeriodScoreAsync(string name, string server,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _cli.GetAsync(
                    $"{_apiHost}/player/period?name={name}&realm={server}", cancellationToken)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Response<PeriodRating?>>(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to get Dungeon info");
            throw;
        }
    }

    public async Task<Response<List<DungeonRating?>>> GetRoleDungeonInfoAsync(string name, string server,
        CancellationToken cancellationToken = default)
    {
        try
        {
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

    public async Task<List<string>> GetServerListAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Response<List<DungeonInfo>?>> GetDungeonListAsync(CancellationToken cancellationToken = default)
    {
        try
        {
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


    public void GetUserBlackListAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void AddUserToBlackListAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}