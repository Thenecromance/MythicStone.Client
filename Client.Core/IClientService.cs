using Client.UI.Model.Dungeon;
using Client.UI.Model.PlayerModel;

namespace Client.Core;

public interface IClientService :
    IPlayerDataSearchService,
    IStaticResourcesService,
    IBlackListService,
    IDisposable
{
    // Task<IEnumerable<PlayerInfo>> GetPlayerInfoAsync(KeyValuePair<string, string> lists,
    //     CancellationToken cancellationToken = default);
}

public interface IPlayerDataSearchService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="server"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Response<PlayerInfo?>> GetPlayerInfoAsync(string name, string server,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// request target player's best score which recorded in the server
    /// </summary>
    /// <param name="name">role's name</param>
    /// <param name="server">role's server</param>
    /// <param name="cancellationToken"></param>
    void GetPlayerBestScoreAsync(string name, string server, CancellationToken cancellationToken = default);

    /// <summary>
    /// request target role's best score which recorded in the server
    /// </summary>
    /// <param name="name"></param>
    /// <param name="server"></param>
    /// <param name="cancellationToken"></param>
    void GetRoleBestScoreAsync(string name, string server, CancellationToken cancellationToken = default);

    void GetRoleBestScoreDetailAsync(string name, string server, CancellationToken cancellationToken = default);

    Task<Response<PeriodRating?>> GetRoleThisPeriodScoreAsync(string name, string server,
        CancellationToken cancellationToken = default);

    Task<Response<List<DungeonRating?>>> GetRoleDungeonInfoAsync(string name, string server,
        CancellationToken cancellationToken = default);
}

public interface IStaticResourcesService
{
    /// <summary>
    /// Get all world of warcraft servers
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<string>> GetServerListAsync(CancellationToken cancellationToken = default);

    Task<Response<List<DungeonInfo>?>> GetDungeonListAsync(CancellationToken cancellationToken = default);
}

public interface IBlackListService
{
    void GetUserBlackListAsync(CancellationToken cancellationToken = default);

    void AddUserToBlackListAsync(CancellationToken cancellationToken = default);
}