using System.Net.Http.Headers;
using System.Net.Http.Json;
using Client.Core.Services;
using Microsoft.Extensions.Logging;

namespace Client.Core;

public class AuthorizationService
{
    public string? UID { get; set; }


    private string _authToken = string.Empty;
    public string Token => _authToken;

    private readonly ILogger _logger;
    private readonly ProfileService _profile = new();

    private bool _isAuthenticated = false;
    public bool IsAuthenticated => _isAuthenticated;


    private readonly string _host =
#if DEBUG
        "http://localhost:8080";
#else
            "https://mythicstone.plus";
#endif


    private readonly HttpClient _cli = new();
    private readonly SemaphoreSlim _authLock = new(1, 1);

    public AuthorizationService(
        ILogger<AuthorizationService> logger)
    {
        _logger = logger;
        _logger.LogInformation("Initializing Authorization Service");


        UID = _profile.UID;
    }


    public void Dispose()
    {
        _cli?.Dispose();
        _authLock?.Dispose();
    }


    public void Authorize()
    {
        try
        {
            _logger.LogInformation("Start Authorization");
            if (_profile.IsFirstTime())
            {
                _logger.LogInformation("Registering new user");
                Register();
            }
            else
            {
                _logger.LogInformation("Logging in");
                Login();
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to authorize");
            throw;
        }
    }

    private void Login()
    {
        LoginAsync().Wait();
    }


    private async Task LoginAsync()
    {
        await _authLock.WaitAsync();
        try
        {
            if (_isAuthenticated)
            {
                return;
            }

            var authData = new Dictionary<string, string?>() { { "uid", UID } };
            var content = new FormUrlEncodedContent(authData);
            var response = await _cli.PostAsync($"{_host}/client/login", content).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to authenticate");
                return;
            }

            var data = await response.Content.ReadFromJsonAsync<Response<Authorization>>();


            if (data is null)
            {
                _logger.LogError("failed to request token");
                return;
            }

            if (data?.Message?.Level != 2)
            {
                _logger.LogError(data.Message.Content);
                return;
            }

            if (data?.Data?.Token is null)
            {
                _logger.LogError("failed to request token");
            }

            _authToken = data.Data.Token;
            UID = data.Data.UID;
            _profile.UID = data.Data.UID;

            _isAuthenticated = true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to authenticate");
        }
        finally
        {
            _authLock.Release();
        }
    }


    private void Register()
    {
        RegisterAsync().Wait();
    }

    private async Task RegisterAsync()
    {
        await _authLock.WaitAsync();
        try
        {
            if (_isAuthenticated)
            {
                return;
            }


            var response = await _cli.PostAsync($"{_host}/client/register", null,
                CancellationToken.None).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to authenticate");
                return;
            }

            var data = await response.Content.ReadFromJsonAsync<Response<Authorization>>();


            if (data is null)
            {
                _logger.LogError("failed to request token");
                return;
            }

            if (data?.Message?.Level != 2)
            {
                _logger.LogError(data.Message.Content);
                return;
            }

            if (data?.Data?.Token is null)
            {
                _logger.LogError("failed to request token");
            }

            _authToken = data.Data.Token;
            UID = data.Data.UID;
            _profile.UID = data.Data.UID;

            _isAuthenticated = true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Failed to authenticate");
        }
        finally
        {
            _authLock.Release();
        }
    }
}