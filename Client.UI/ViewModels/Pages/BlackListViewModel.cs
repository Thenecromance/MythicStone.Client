using Client.UI.Model.BlackList;
using Client.UI.Services;

namespace Client.UI.ViewModels.Pages;

public class BlackListViewModel : ViewModel
{
    private readonly ClientService _cli;

    public BlackListViewModel(ClientService clientService)
    {
        _cli = clientService;


        if (SuspendPlayers.Count == 0)
        {
            UpdateSuspendPlayerList();
        }
    }

    private async void UpdateSuspendPlayerList()
    {
        var result = await _cli.GetUserBlackListAsync();
        if (result != null)
            foreach (var player in result)
            {
                SuspendPlayers.Add(player);
            }
    }


    private ObservableCollection<SuspendPlayer?> _suspendPlayers = [];

    public ObservableCollection<SuspendPlayer?> SuspendPlayers
    {
        get => _suspendPlayers;
        set => SetProperty(ref _suspendPlayers, value);
    }
}