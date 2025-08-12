using System;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Songdle.Infrastructure.Spotify.Options;

namespace Songdle.Infrastructure.Spotify;


public class SpotifyAuthService(IOptions<SpotifyOptions> options)
{

    private readonly string clientId = options.Value.ClientId ?? throw new ArgumentNullException(nameof(clientId));
    private readonly string clientSecret = options.Value.ClientSecret ?? throw new ArgumentNullException(nameof(clientSecret));
    
    private string _accessToken;
    private DateTime _tokenExpiration;

    public async Task<string> GetAccessTokenAsync()
    {
        if (!string.IsNullOrEmpty(_accessToken) && DateTime.UtcNow < _tokenExpiration)
            return _accessToken;

        using var client = new HttpClient();
        var byteArray = System.Text.Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}");
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));

        var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token")
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "grant_type", "client_credentials" }
                })
        };

        var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadFromJsonAsync<SpotifyTokenResponse>();

        _accessToken = json.access_token;
        _tokenExpiration = DateTime.UtcNow.AddSeconds(json.expires_in - 30);

        return _accessToken;
    }

    private class SpotifyTokenResponse
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }
}

