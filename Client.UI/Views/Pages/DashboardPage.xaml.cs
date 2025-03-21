using Client.UI.ViewModels.Pages;
using Wpf.Ui.Abstractions.Controls;


namespace Client.UI.Views.Pages
{
    /// <summary>
    /// DashboardPage.xaml 的交互逻辑
    /// </summary>
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        public DashboardPage(DashboardViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;


            InitializeComponent();
        }

        public DashboardViewModel ViewModel { get; }
    }
}