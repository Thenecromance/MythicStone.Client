using Client.UI.Model.Dungeon;
using Client.UI.Services;

namespace Client.UI.ViewModels.Pages;

public class DungeonListViewModel : ViewModel
{
    private readonly ClientService _cli;

    public DungeonListViewModel(ClientService cli)
    {
        _cli = cli;
    }

    /*
    public async void UpdateDungeonList()
    {
        if (_dungeonList.Count != 0)
        {
            return;
        }

        var dungeons = await _cli.GetDungeonListAsync();
        foreach (var dungeon in dungeons)
        {
            _dungeonList.Add(dungeon);
        }
    }*/
}