using System.Windows.Controls;
using Client.Connector.Simc.ViewModels;

namespace Client.Connector.Simc.Views;

public partial class SimcRequestInfoPage : Page
{
    public SimcRequestViewModel ViewModel { get; }

    public SimcRequestInfoPage(SimcRequestViewModel vm)
    {
        ViewModel = vm;
        InitializeComponent();

        Loaded += (s, e) => DataContext = this;
    }
}