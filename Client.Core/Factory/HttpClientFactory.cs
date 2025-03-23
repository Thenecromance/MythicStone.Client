using System.Net.Http.Headers;

namespace Client.Core.Factory;

public class HttpClientFactory : IHttpClientFactory
{
    Dictionary<string, HttpClient> _clients = new Dictionary<string, HttpClient>();

    private void SetDefaultHeaders(ref HttpClient cli)
    {
        cli.DefaultRequestHeaders.Clear();

        cli.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        cli.DefaultRequestHeaders.Add("User-Agent", "Client");
        cli.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
        cli.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));

        if (Token != string.Empty)
        {
            cli.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
        }

        if (ClientId != string.Empty)
        {
            cli.DefaultRequestHeaders.Add("Client-Id", ClientId);
        }
    }

    private string Token { get; set; } = string.Empty;
    private string? ClientId { get; set; } = string.Empty;


    private readonly AuthorizationService _auth;

    public HttpClientFactory(AuthorizationService auth)
    {
        _auth = auth;
        if (!_auth.IsAuthenticated) return;
        Token = _auth.Token;
        ClientId = _auth.UID;
    }


    public HttpClient CreateClient(string name)
    {
        if (!_auth.IsAuthenticated)
        {
            _auth.Authorize();
            Token = _auth.Token;
            ClientId = _auth.UID;
        }


        HttpClient cli;
        if (_clients.TryGetValue(name, out var client))
        {
            cli = client;
        }
        else
        {
            cli = new HttpClient();
            _clients.Add(name, cli);
        }

        SetDefaultHeaders(ref cli);

        return cli;
    }
}