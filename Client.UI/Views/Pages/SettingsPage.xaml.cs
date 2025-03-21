using Client.UI.ViewModels.Pages;
using System.Windows.Controls;
using Client.UI.ViewModels;
using Wpf.Ui.Abstractions.Controls;

namespace Client.UI.Views.Pages;


public partial class SettingsPage : INavigableView<SettingsViewModel>
{
    public SettingsViewModel ViewModel { get; }

    public SettingsPage(SettingsViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}
