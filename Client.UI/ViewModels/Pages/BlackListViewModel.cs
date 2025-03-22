using Client.UI.Services;

namespace Client.UI.ViewModels.Pages;

public class BlackListViewModel : ViewModel
{
    private readonly ClientService _cli;

    public BlackListViewModel(ClientService clientService)
    {
        _cli = clientService;
    }
}