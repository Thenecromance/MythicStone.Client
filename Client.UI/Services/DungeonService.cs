using Client.UI.Model.Dungeon;

namespace Client.UI.Services;

public class DungeonService
{
    private ClientService Cli { get; } = App.GetRequiredService<ClientService>();
    private bool _initialized = false;

    private List<DungeonInfo> DungeonList { get; } = new List<DungeonInfo>();

    public DungeonService(ClientService cli)
    {
        Cli = cli;

        if (!_initialized)
        {
            InitializeAsync();
            _initialized = true;
        }
    }

    private async void InitializeAsync()
    {
        Debug.WriteLine("Start to get dungeon list");
        
        var dungeons = await Cli.GetDungeonListAsync();
        if (dungeons == null) return;
        foreach (var dungeon in dungeons)
        {
            DungeonList.Add(dungeon);
        }
    }



    public List<DungeonInfo> GetDungeons()
    {
        return DungeonList;
    }
}