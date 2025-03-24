using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Client.Connector.Simc.ViewModels;

namespace Client.Connector.Simc.Views;

public partial class SimcAddonImportPage : Page
{
    public SimcAddonImportViewModel ViewModel { get; }

    public SimcAddonImportPage(SimcAddonImportViewModel vm)
    {
        ViewModel = vm;
        InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        TextRange textRange = new TextRange(ProfileTextBox.Document.ContentStart, ProfileTextBox.Document.ContentEnd);
        ViewModel.Profile = textRange.Text;
        ViewModel.Test();
    }

}