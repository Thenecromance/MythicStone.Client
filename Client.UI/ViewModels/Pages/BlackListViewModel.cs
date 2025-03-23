using Client.UI.Model.BlackList;
using Client.UI.Services;

namespace Client.UI.ViewModels.Pages;

public partial class BlackListViewModel : ViewModel
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


    [RelayCommand]
    public async Task OnRemovePlayerFromBlackList(object sender)
    {
        var uiMessageBox = new Wpf.Ui.Controls.MessageBox
        {
            Title = "警告！",
            Content =
                $"你确定要将 {sender} 从你的黑名单中移除吗？",
            PrimaryButtonText = "确定",
            SecondaryButtonText = "取消"
        };

        Wpf.Ui.Controls.MessageBoxResult result = await uiMessageBox.ShowDialogAsync();
        if (result == Wpf.Ui.Controls.MessageBoxResult.Primary)
        {
            var player = sender as string;
            var realm = string.Empty;
            if (player != null)
            {
                for (var i = 0; i < SuspendPlayers.Count; i++)
                {
                    if (SuspendPlayers[i]?.Name != player) continue;
                    realm = SuspendPlayers[i]?.Realm;
                    break;
                }

                if (realm == null) return;

                var response = await _cli.RemoveUserFromBlackListAsync(player, realm);


                if (response != null && response.Success)
                {
                    for (var i = 0; i < SuspendPlayers.Count; i++)
                    {
                        if (SuspendPlayers[i]?.Name != player) continue;
                        SuspendPlayers.RemoveAt(i);
                        break;
                    }
                }
            }
        }
    }
}