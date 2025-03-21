using System.Windows.Controls;
using System.Windows.Navigation;
using Client.UI.Model.PlayerModel;
using Client.UI.ViewModels.Pages;
using Wpf.Ui;
using Wpf.Ui.Abstractions.Controls;

namespace Client.UI.Views.Pages;

public partial class PlayerDetailPage : INavigableView<PlayerDetailViewModel>
{
    public PlayerDetailViewModel ViewModel { get; }

    public PlayerDetailPage(
        PlayerDetailViewModel viewModel)
    {
        ViewModel = viewModel;
        InitializeComponent();


        Loaded += (s, e) => DataContext = this;
    }
}