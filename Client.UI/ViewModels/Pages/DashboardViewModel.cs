using Client.UI.Helpers;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui;

namespace Client.UI.ViewModels.Pages;

public partial class DashboardViewModel(INavigationService navigationService) : ViewModel
{
    [RelayCommand]
    private void OnCardClick(string parameter)
    {
        if (string.IsNullOrWhiteSpace(parameter))
        {
            return;
        }

        Type? pageType = NameToPageTypeConverter.Convert(parameter);

        if (pageType == null)
        {
            return;
        }

        _ = navigationService.Navigate(pageType);
    }
}