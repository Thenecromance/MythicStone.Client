using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.Connector.Simc.Views;
using Client.UI.Views.Pages;
using Wpf.Ui.Controls;

namespace Client.UI.ViewModels.Windows;

public partial class MainWindowViewModel : ViewModel
{
    [ObservableProperty] private string _applicationTitle = "MythicStone.Client";

    [ObservableProperty] private ObservableCollection<object> _menuItems =
    [
        new NavigationViewItem("主页", SymbolRegular.Home24, typeof(DashboardPage)),
        new NavigationViewItem("查询", SymbolRegular.Airplane24, typeof(PlayerSearchPage)),
        new NavigationViewItem("黑名单", SymbolRegular.LockClosed24, typeof(BlackListPage)),
        // new NavigationViewItem("地下城", SymbolRegular.Cube24, typeof(DungeonPage)),
        // new NavigationViewItem("角色", SymbolRegular.People24, typeof(PlayerDetailPage)),

        new NavigationViewItemSeparator(),

        new NavigationViewItem
        {
            Content = "Simc",
            Icon = new SymbolIcon { Symbol = SymbolRegular.Table24 },
            MenuItemsSource = new object[]
            {
                new NavigationViewItem("英雄榜导入", SymbolRegular.Calculator24, typeof(SimcRequestInfoPage)),
                new NavigationViewItem("插件导入", SymbolRegular.Pause24, typeof(SimcAddonImportPage)),
            },
        },
    ];

    [ObservableProperty] private ObservableCollection<object> _footerMenuItems =
    [
        new NavigationViewItem("设置", SymbolRegular.Settings24, typeof(SettingsPage)),
    ];
}