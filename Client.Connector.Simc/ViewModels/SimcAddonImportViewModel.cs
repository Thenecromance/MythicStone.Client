using System.Configuration;
using System.IO;
using Client.Connector.Simc.Services;
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

    private readonly SimcConnectorService _simcConnectorService;

    public SimcAddonImportViewModel(SimcConnectorService simcConnectorService)
    {
        _simcConnectorService = simcConnectorService;
    }

    public void Test()
    {
        string userTempPath = Path.GetTempPath();
        File.WriteAllText(userTempPath + "simcProfile.simc", Profile);
        _simcConnectorService.Execute(userTempPath + "simcProfile.simc");
    }
}