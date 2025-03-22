using System.Windows.Controls;
using Client.UI.ViewModels.Pages;

namespace Client.UI.Views.Pages;

public partial class BlackListPage : Page
{
    public BlackListViewModel ViewModel { get; }

    public BlackListPage(BlackListViewModel vm
    )
    {
        ViewModel = vm;
        InitializeComponent();
        
        Loaded += (s, e) => DataContext = this;
    }
}