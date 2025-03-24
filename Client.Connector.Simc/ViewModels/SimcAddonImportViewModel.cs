using System.Configuration;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Client.Connector.Simc.ViewModels;

public class SimcAddonImportViewModel : ViewModel
{
    private string _profile;

    public string Profile
    {
        get => _profile;
        set => SetProperty(ref _profile, value);
    }
}