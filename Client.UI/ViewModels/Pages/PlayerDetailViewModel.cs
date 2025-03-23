using System.Windows.Controls;
using System.Windows.Navigation;
using Client.Core;
using Client.UI.Controls;
using Client.UI.Model.Dungeon;
using Client.UI.Model.PlayerModel;
using Client.UI.Services;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;
using TextBlock = Wpf.Ui.Controls.TextBlock;

namespace Client.UI.ViewModels.Pages;

public partial class PlayerDetailViewModel
    : ViewModel
{
    private ClientService _cli { get; }
    private PlayerSearchViewModel _playerSearchViewModel { get; }
    private IContentDialogService _contentDialogService { get; }


    public PlayerDetailViewModel(
        ClientService cli,
        IContentDialogService contentDialogService,
        PlayerSearchViewModel playerSearchViewModel)
    {
        _cli = cli;
        _contentDialogService = contentDialogService;
        this._playerSearchViewModel = playerSearchViewModel;
    }

    private async void InitializeAsync()
    {
        // var dungeons = await Cli.GetDungeonListAsync();
        // foreach (var dungeon in dungeons)
        // {
        //     Console.WriteLine(dungeon.Name);
        //     DungeonList.Add(dungeon);
        // }

        RequestCharacterPeriodDungeonRatingAsync();
        RequestCharacterDungeonRatingAsync();
    }

    private PlayerInfo _info = new PlayerInfo();

    public PlayerInfo Info
    {
        get => _info;
        private set => _ = SetProperty(ref _info, value);
    }

    #region PlayerDungeonInfo

    private ObservableCollection<PlayerDungeonInfo> _playerDungeonInfo = new ObservableCollection<PlayerDungeonInfo>();

    public ObservableCollection<PlayerDungeonInfo> PlayerDungeonInfo
    {
        get => _playerDungeonInfo;
        set { SetProperty(ref _playerDungeonInfo, value); }
    }

    #endregion

    public override void OnNavigatedTo()
    {
        base.OnNavigatedTo();
        Info = _playerSearchViewModel.GetCurrentPlayer();

        InitializeAsync();
    }


    #region DungeonList

    private ObservableCollection<DungeonInfo> _dungeonList = new ObservableCollection<DungeonInfo>();

    public ObservableCollection<DungeonInfo> DungeonList
    {
        get => _dungeonList;
        set => SetProperty(ref _dungeonList, value);
    }

    #endregion

    #region PeriodDungeonRating

    private ObservableCollection<DungeonRating> _periodDungeonRating = new ObservableCollection<DungeonRating>();

    public ObservableCollection<DungeonRating> PeriodRatings
    {
        get => _periodDungeonRating;
        set => SetProperty(ref _periodDungeonRating, value);
    }

    private async void RequestCharacterPeriodDungeonRatingAsync()
    {
        PeriodRatings.Clear();
        var period = await _cli.GetRoleThisPeriodScoreAsync(Info.Name, Info.Realm);
        if (period == null) return;

        foreach (var dungeon in period.Dungeons)
        {
            PeriodRatings.Add(dungeon);
        }
    }

    #endregion

    #region DungeonRating

    private ObservableCollection<DungeonRating> _roleDungeonRating = new ObservableCollection<DungeonRating>();

    public ObservableCollection<DungeonRating> RoleRatings
    {
        get => _roleDungeonRating;
        set => SetProperty(ref _roleDungeonRating, value);
    }

    private async void RequestCharacterDungeonRatingAsync()
    {
        RoleRatings.Clear();
        var dungoenRating = await _cli.GetRoleDungeonInfoAsync(Info.Name, Info.Realm);
        if (dungoenRating == null) return;

        // foreach (var dungeon in dungoenRating)
        // {
        //     RoleRatings.Add(dungeon);
        // }

        Dictionary<int, DungeonRating> dict = new Dictionary<int, DungeonRating>();

        foreach (var dungeon in dungoenRating)
        {
            if (dict.ContainsKey(dungeon.DungeonId))
            {
                if (dict[dungeon.DungeonId].Rating < dungeon.Rating)
                {
                    dict[dungeon.DungeonId] = dungeon;
                }
            }
            else
            {
                dict.Add(dungeon.DungeonId, dungeon);
            }
        }


        foreach (var dungeon in dict.Values)
        {
            RoleRatings.Add(dungeon);
        }
    }

    #endregion

    #region HeroLink

    [RelayCommand]
    private async Task OnOpenPlayerLink(object sender)
    {
        Task.Run(() =>
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = Info.Link,
                UseShellExecute = true
            });
        });
    }

    #endregion


    #region BlackList

    private string _reason = string.Empty;

    [RelayCommand]
    private async Task OnAddBlackList(object sender)
    {
        var dialog =
            new AddToBlackListContentDialog(_contentDialogService.GetDialogHost(), $"{Info.Name}-{Info.Realm}");
        ;
        var result = await dialog.ShowAsync();
        if (result == ContentDialogResult.Primary)
        {
            var opResult = await _cli.AddUserToBlackListAsync(Info.Name, Info.Realm, dialog.Reason);
            if (opResult != null && opResult.Success == false)
            {
                var uiMessageBox = new Wpf.Ui.Controls.MessageBox
                {
                    Title = "警告！",
                    Content =
                        $"{Info.Name} 加入黑名单失败",
                    PrimaryButtonText = "确定",
                    SecondaryButtonText = "取消"
                };
                await uiMessageBox.ShowDialogAsync();
            }
        }
    }

    #endregion
}