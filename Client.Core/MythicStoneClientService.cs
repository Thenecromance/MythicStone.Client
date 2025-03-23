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

public sealed class MythicStoneClientService(
    ILogger<MythicStoneClientService> logger,
    IHttpClientFactory httpClientFactory)
    :
        IPlayerDataSearchService,
        IStaticResourcesService,
        IBlackListService
{
    private static string _host =
#if DEBUG
        "http://localhost:8080";
#else
            "https://mythicstone.plus";
#endif


    // public bool _readyToUse = false;

    private string ApiHost { get; } = $"{_host}/api/v1";


    public async Task<Response<PlayerInfo?>?> GetPlayerInfoAsync(string name, string server,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await httpClientFactory.CreateClient("PlayerInfo").GetAsync(
                    $"{ApiHost}/player/info?name={name}&realm={server}", cancellationToken)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Response<PlayerInfo?>>(cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to get player info");
            throw;
        }
    }

    public async Task<Response<PeriodRating?>?> GetRoleThisPeriodScoreAsync(string name, string server,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await httpClientFactory.CreateClient("PeriodScore").GetAsync(
                    $"{ApiHost}/player/period?name={name}&realm={server}", cancellationToken)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Response<PeriodRating?>>(cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "fail to get period score");
            throw;
        }
    }

    public async Task<Response<List<DungeonRating?>>?> GetRoleDungeonInfoAsync(string name, string server,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await httpClientFactory.CreateClient("RoleDungeon").GetAsync(
                    $"{ApiHost}/player/dungeon?name={name}&realm={server}", cancellationToken)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Response<List<DungeonRating?>>>(cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to get Dungeon info");
            throw;
        }
    }

    public async Task<Response<List<DungeonInfo>?>?> GetDungeonListAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await httpClientFactory.CreateClient("DungeonList").GetAsync(
                    $"{ApiHost}/dungeon/list", cancellationToken)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Response<List<DungeonInfo>?>>(cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to get Dungeon info");
            throw;
        }
    }


    public async Task<Response<List<SuspendPlayer>?>?> GetUserBlackListAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await httpClientFactory.CreateClient("BlackList").GetAsync(
                    $"{ApiHost}/blacklist", cancellationToken)
                .ConfigureAwait(false);

            return await response.Content.ReadFromJsonAsync<Response<List<SuspendPlayer>?>>(
                cancellationToken: cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to get black list");
            throw;
        }
    }


    public async Task<Message?> AddUserToBlackListAsync(string name, string realm, string reason,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var parameters = new Dictionary<string, string>
            {
                { "name", name },
                { "realm", realm },
                { "reason", reason },
            };

            var content = new FormUrlEncodedContent(parameters);


            var response = await httpClientFactory.CreateClient("BlackList").PostAsync(
                    $"{ApiHost}/blacklist", content, cancellationToken)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Message?>(cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to get black list");
            throw;
        }
    }

    public async void RemoveUserFromBlackListAsync(string name, string realm,
        CancellationToken cancellationToken = default)
    {
        var response = await httpClientFactory.CreateClient("BlackList").DeleteAsync(
                $"{ApiHost}/blacklist?name={name}&realm={realm}", cancellationToken)
            .ConfigureAwait(false);
        response.EnsureSuccessStatusCode();
        // return await response.Content.ReadFromJsonAsync<Response<List<SuspendPlayer>?>>();
    }
}