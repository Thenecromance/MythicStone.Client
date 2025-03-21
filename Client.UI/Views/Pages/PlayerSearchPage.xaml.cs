using Client.UI.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;

namespace Client.UI.Views.Pages;

public partial class PlayerSearchPage : INavigableView<PlayerSearchViewModel>
{
    public PlayerSearchPage(PlayerSearchViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;
        InitializeComponent();
    }

    public PlayerSearchViewModel ViewModel { get; }


    private void AutoSuggestBox_TextChanged(Wpf.Ui.Controls.AutoSuggestBox sender,
        Wpf.Ui.Controls.AutoSuggestBoxTextChangedEventArgs args)
    {
        //get input text
        var input = sender.Text;
        if (!input.EndsWith('-'))
        {
            return;
        }

        var split = input.Split('-');
        ViewModel.UpdateAutoSuggestBox(split[0], split[1]);
    }


    private void AutoSuggestBox_QuerySubmitted(Wpf.Ui.Controls.AutoSuggestBox sender,
        Wpf.Ui.Controls.AutoSuggestBoxQuerySubmittedEventArgs args)
    {
        if (!args.QueryText.Contains('-')) return;
        var splits = args.QueryText.Split('-');
        ViewModel.SearchPlayer(splits[0], splits[1]);
    }
}