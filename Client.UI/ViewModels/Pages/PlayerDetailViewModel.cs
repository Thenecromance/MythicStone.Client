using System.Windows.Navigation;
using Client.Core;
using Client.UI.Model.Dungeon;
using Client.UI.Model.PlayerModel;
using Client.UI.Services;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace Client.UI.ViewModels.Pages;

public class PlayerDetailViewModel
    : ViewModel
{
    public ClientService Cli { get; }
    private PlayerSearchViewModel playerSearchViewModel { get; }


    public PlayerDetailViewModel(
        ClientService cli,
        PlayerSearchViewModel playerSearchViewModel)
    {
        Cli = cli;
        this.playerSearchViewModel = playerSearchViewModel;
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
        Info = playerSearchViewModel.GetCurrentPlayer();

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
        var period = await Cli.GetRoleThisPeriodScoreAsync(Info.Name, Info.Realm);
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
        var dungoenRating = await Cli.GetRoleDungeonInfoAsync(Info.Name, Info.Realm);
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
}


/*

{
            "dungeon_id": 247,
            "season": 14,
            "completed_timestamp": 1742117923000,
            "duration": 1995667,
            "is_completed": false,
            "keystone_level": 10,
            "rating": 304.70328
        },

        */