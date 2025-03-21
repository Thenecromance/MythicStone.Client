using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Client.UI.Model.Dungeon;
using Client.UI.Model.PlayerModel;
using Microsoft.Extensions.Logging;
using Wpf.Ui;

namespace Client.Core;

// public class MythicSonteClientService : IClientService
// {
//     public string? ApiKey { get; set; }
//
//
//     private readonly string baseUrl =
// #if DEBUG
//         "http://localhost:8080/api/v1";
// #else
//         "http://mythic_stone.plus/api/v1";
// #endif
//     private readonly HttpClient _cli;
//
//     public MythicSonteClientService()
//     {
//         _cli = new HttpClient();
//     }
//
//
//     public async Task<PlayerInfo> GetPlayerInfoAsync(string name, string realm)
//     {
//         var response = await SearchPlayerInfoAsync(name, realm);
//         Console.WriteLine(response);
//
//         return response;
//     }
//
//     public async Task<BestScore> GetBestScoreAsync(string name, string realm)
//     {
//         throw new NotImplementedException();
//     }
//
//     public async Task<SelfScore> GetSelfScoreAsync(string name, string realm)
//     {
//         throw new NotImplementedException();
//     }
//
//
//     private async Task<PlayerInfo> SearchPlayerInfoAsync(string name, string realm)
//     {
//         string url = $"{baseUrl}/player/info?name={name}&realm={realm}&apikey={ApiKey}";
//         try
//         {
//             var response = await _cli.GetAsync(url);
//             response.EnsureSuccessStatusCode();
//
//             var stream = await response.Content.ReadAsStreamAsync();
//             return await JsonSerializer.DeserializeAsync<PlayerInfo>(stream);
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//
//             return new PlayerInfo();
//         }
//
//         /*if (response.IsSuccessStatusCode)
//         {
//             var content = await response.Content.ReadAsStringAsync();
//             return JsonSerializer.Deserialize<>(content);
//         }*/
//
//         // return default;
//     }
// }

public sealed class MythicStoneClientService : IClientService
{
    private readonly HttpClient _cli = new HttpClient();
    private readonly ILogger<MythicStoneClientService> _logger;

    private readonly string host =
#if DEBUG
        "http://localhost:8080/api/v1";
#else
            "http://mythic_stone.plus/api/v1";
#endif

    public MythicStoneClientService(ILogger<MythicStoneClientService> logger)
    {
        _logger = logger;

        _cli.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public void Dispose() => _cli?.Dispose();


    public async Task<Response<PlayerInfo?>> GetPlayerInfoAsync(string name, string server,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _cli.GetAsync(
                    $"{host}/player/info?name={name}&realm={server}", cancellationToken)
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
                    $"{host}/player/period?name={name}&realm={server}", cancellationToken)
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
                    $"{host}/player/dungeon?name={name}&realm={server}", cancellationToken)
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
                    $"{host}/dungeon/list", cancellationToken)
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