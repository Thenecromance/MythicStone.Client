using System.Diagnostics.CodeAnalysis;

namespace Client.UI.ViewModels.Pages;

public partial class MessageBoxViewModel : ViewModel
{
    [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "relay command")]
    [RelayCommand]
    private async Task OnOpenCustomMessageBox(object sender)
    {
    }
}