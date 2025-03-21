using System.Windows.Controls;
using Client.Core;
using Client.UI.Model.PlayerModel;
using Client.UI.PedPool;
using Client.UI.Services;
using Client.UI.Views.Pages;
using Microsoft.Extensions.Logging;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Client.UI.ViewModels.Pages;

public partial class PlayerSearchViewModel : ViewModel
{
    private readonly ILogger<PlayerSearchViewModel> _logger;

    public PlayerSearchViewModel(
        ClientService cli,
        INavigationService navigationService,
        ILogger<PlayerSearchViewModel> logger,
        PlayerPool playerPool)
    {
        Cli = cli;
        NavigationService = navigationService;
        _logger = logger;

        SearchHistoryListView.CollectionChanged += (sender, args) => { SearchHistoryVisible = true; };
    }


    #region SearchHistorySelectedIndex

    private int _searchHistorySelectedIndex = 0;

    public int SearchHistorySelectedIndex
    {
        get => _searchHistorySelectedIndex;
        set => _ = SetProperty(ref _searchHistorySelectedIndex, value);
    }

    #endregion

    [ObservableProperty] private SelectionMode _listViewSelectionMode = SelectionMode.Single;

    #region SearchHistoryListView

    private ObservableCollection<PlayerInfo?> _searchHistoryListView = [];

    public ObservableCollection<PlayerInfo?> SearchHistoryListView
    {
        get => _searchHistoryListView;
        set => SetProperty(ref _searchHistoryListView, value);
    }

    #endregion

    #region SearchHistoryVisible

    private bool _searchHistoryVisible = false;

    public bool SearchHistoryVisible
    {
        set => _ = SetProperty(ref _searchHistoryVisible, value);
    }

    #endregion


    /*
     * SearchComponent Support
     */

    #region Suggestions

    private ObservableCollection<string> _autoSuggestBoxSuggestions = [];

    public ObservableCollection<string> AutoSuggestBoxSuggestions
    {
        get => _autoSuggestBoxSuggestions;
        set => SetProperty(ref _autoSuggestBoxSuggestions, value);
    }

    #endregion

    #region RequestInProgress

    private bool _requestInProgress = false;

    public bool RequestInProgress
    {
        get => !_requestInProgress;
        set => SetProperty(ref _requestInProgress, value);
    }

    #endregion


    #region ClientService

    private ClientService Cli { get; }

    public void UpdateAutoSuggestBox(string name, string server)
    {
        AutoSuggestBoxSuggestions.Clear();
        var list = FindAllPrefix(server);
        foreach (var realm in list)
        {
            AutoSuggestBoxSuggestions.Add($"{name}-{realm}");
        }
    }

    private static List<string> FindAllPrefix(string server)
    {
        var result = new List<string>();
        Data.ServerList.Where(x => x.StartsWith(server)).ToList().ForEach(x => result.Add(x));
        return result;
    }


    public async void SearchPlayer(string name, string realm)
    {
        try
        {
            for (var i = 0; i < SearchHistoryListView.Count; i++)
            {
                if (_searchHistoryListView[i]?.Name != name) continue;
                _searchHistoryListView.Move(i, 0);
                return;
            }


            RequestInProgress = true;
            var player = await Cli.GetPlayerInfoAsync(name, realm);

            if (player != null && player.Class != 0)
            {
                SearchHistoryListView.Insert(0, player);
            }

            if (player?.BannedInfo is not null)
            {
                ShowBanMessageBox(player);
            }

            RequestInProgress = false;
        }
        catch (Exception e)
        {
            _logger.LogError($"Failed to search player with error:{e.Message}");
            RequestInProgress = false;
        }
    }

    private async void ShowBanMessageBox(PlayerInfo player)
    {
        try
        {
            var uiMessageBox = new Wpf.Ui.Controls.MessageBox
            {
                Title = "MythicStone.plus 黑名单警告！",
                Content =
                    $"该用户在你的黑名单中！\n" +
                    $"角色名： {player.BannedInfo?.Name}\n " +
                    $"服务器名：{player.BannedInfo?.Realm}\n" +
                    $"原因：{player.BannedInfo?.Reason}",
            };

            _ = await uiMessageBox.ShowDialogAsync();
        }
        catch (Exception e)
        {
            _logger.LogError($"Failed to show ban message box. with error:{e.Message}");
        }
    }

    #endregion


    #region NavigationService

    private INavigationService NavigationService { get; }


    private PlayerInfo _current = new PlayerInfo();

    [RelayCommand]
    private void NavigateToPlayerDetailPage(string uuid)
    {
        for (var i = 0; i < SearchHistoryListView.Count; i++)
        {
            if (_searchHistoryListView[i]?.UUID == uuid)
            {
                _current = _searchHistoryListView[i] ?? throw new InvalidOperationException();
            }
        }

        NavigationService.NavigateWithHierarchy(typeof(PlayerDetailPage));
    }

    public PlayerInfo GetCurrentPlayer()
    {
        return _current;
    }

    #endregion
}