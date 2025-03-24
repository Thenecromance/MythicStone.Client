using System.Net.Http;
using Client.Connector.Simc.Services;
using Client.Connector.Simc.ViewModels;
using Client.Connector.Simc.Views;
using Client.Core;
using Client.Core.Factory;
using Client.Core.Services;
using Client.UI.Services;
using Client.UI.Services.Contracts;
using Client.UI.ViewModels;
using Client.UI.ViewModels.Pages;
using Client.UI.ViewModels.Windows;
using Client.UI.Views.Pages;
using Client.UI.DependencyModel;
using Client.UI.Factory;
using Client.UI.Views.Window;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Wpf.Ui;
using Wpf.Ui.DependencyInjection;

namespace Client.UI;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private static readonly IHost _host = Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration(c => { _ = c.SetBasePath(AppContext.BaseDirectory); }).ConfigureServices(
            (_1, services) =>
            {
                services.AddNavigationViewPageProvider();
                services.AddHostedService<ApplicationHostService>();


                // Main window container with navigation
                services.AddSingleton<IWindow, MainWindow>();
                services.AddSingleton<MainWindowViewModel>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<ISnackbarService, SnackbarService>();
                services.AddSingleton<IContentDialogService, ContentDialogService>();
                services.AddSingleton<WindowsProviderService>();

                services.AddTransient<INLogConfigFactory, NLogConfigFactory>();

                // profile settings 
                services.AddSingleton<AuthorizationService>();
                services.AddSingleton<IHttpClientFactory, HttpClientFactory>();


                // Top-level pages
                services.AddSingleton<DashboardPage>();
                services.AddSingleton<DashboardViewModel>();

                services.AddSingleton<SettingsPage>();
                services.AddSingleton<SettingsViewModel>();


                services.AddSingleton<Core.MythicStoneClientService>();
                services.AddSingleton<ClientService>();


                services.AddSingleton<DungeonService>();
                services.AddSingleton<PlayerSearchPage>();
                services.AddSingleton<PlayerSearchViewModel>();
                // services.AddSingleton<PlayerDetailPage>();
                services.AddSingleton<PlayerDetailViewModel>();

                // Simc pages and view models and services
                {
                    // Simc request from server 
                    services.AddSingleton<SimcRequestInfoPage>();
                    services.AddSingleton<SimcRequestViewModel>();
                    //simc addon import
                    services.AddSingleton<SimcAddonImportPage>();
                    services.AddSingleton<SimcAddonImportViewModel>();

                    services.AddSingleton<SimcConnectorService>();
                }


                // All other pages and view models
                services.AddTransientFromNamespace("Client.UI.Views", GalleryAssembly.Asssembly);
                services.AddTransientFromNamespace(
                    "Client.UI.ViewModels",
                    GalleryAssembly.Asssembly
                );
                services.AddSingleton(provider =>
                {
                    var configFactory = provider.GetRequiredService<INLogConfigFactory>();
                    var logFolderPath = "logs";

                    return LoggerFactory.Create(builder =>
                    {
                        builder.ClearProviders();
                        // Ref: https://github.com/NLog/NLog.Extensions.Logging/issues/389
                        builder.SetMinimumLevel(LogLevel.Trace);
                        builder.AddNLog(configFactory.Create(logFolderPath));
                    });
                });
            })
        .Build();


    /// <summary>
    /// Gets registered service.
    /// </summary>
    /// <typeparam name="T">Type of the service to get.</typeparam>
    /// <returns>Instance of the service or <see langword="null"/>.</returns>
    public static T GetRequiredService<T>()
        where T : class
    {
        return _host.Services.GetRequiredService<T>();
    }

    private void OnStartUp(object sender, StartupEventArgs e)
    {
        _host.Start();
    }

    /// <summary>
    /// Occurs when the application is closing.
    /// </summary>
    private void OnExit(object sender, ExitEventArgs e)
    {
        _host.StopAsync().Wait();

        _host.Dispose();
    }


    /// <summary>
    /// Occurs when an exception is thrown by an application but not handled.
    /// </summary>
    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        GetRequiredService<ILogger<App>>().LogError("Unhandled exception: {0}", e.Exception);
        e.Handled = true;
    }
}