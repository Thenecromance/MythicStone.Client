using Client.UI.Model.PlayerModel;
using Microsoft.Extensions.Logging;

namespace Client.UI.PedPool;

public class PlayerPool
{
    private readonly ILogger<PlayerPool> _logger;

    private readonly Dictionary<string, PlayerInfo> _playerMap = new();

    private string _selectedPlayer = string.Empty;


    public PlayerPool(ILogger<PlayerPool> logger)
    {
        _logger = logger;

        _logger.LogInformation("Player pool created.");
    }

    public string AddPlayer(PlayerInfo? player)
    {
        if (player is null)
        {
            _logger.LogWarning("Attempted to add a null player to the player pool.");
            return string.Empty;
        }

        _logger.LogInformation("Added player {PlayerName} to the player pool.", player.Name);

        var uuid = Guid.NewGuid().ToString();
        _playerMap[uuid] = player;
        _selectedPlayer = uuid;
        return uuid;
    }

    public PlayerInfo? GetCurrentPlayer()
    {
        if (_selectedPlayer == string.Empty)
        {
            _logger.LogWarning("Attempted to get the current player when no player is selected.");
            return null;
        }

        if (_playerMap.TryGetValue(_selectedPlayer, out var player)) return player;
        _logger.LogWarning("Attempted to get the current player when the selected player does not exist.");
        return null;
    }

    public PlayerInfo? TryGetPlayer(string name, string realm)
    {
        foreach (var player in _playerMap.Values)
        {
            if (player.Name == name && player.Realm == realm)
            {
                return player;
            }
        }

        return null;
    }

    public ObservableCollection<PlayerInfo?> GetPlayers()
    {
        return new ObservableCollection<PlayerInfo?>(_playerMap.Values);
    }
}