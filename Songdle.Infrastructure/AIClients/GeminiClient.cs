using System;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Songdle.Infrastructure.AIClients.Options;

namespace Songdle.Infrastructure.AIClients;

public class GeminiClient(HttpClient httpClient, IOptions<GeminiOptions> options)
{
    public async Task<string> GetResponseAsync(string prompt)
    {
        var apiKey = options.Value.ApiKey ?? throw new ArgumentNullException(nameof(options.Value.ApiKey));
        var model = options.Value.Model ?? throw new ArgumentNullException(nameof(options.Value.Model));

        var url = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={apiKey}";

        var requestBody = new
        {
            contents = new[]
           {
                new { parts = new[] { new { text = prompt } } }
            }
        };

        var json = JsonSerializer.Serialize(requestBody);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, content);

        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();

        return responseBody;

    }

    public string GetResponseText(string jsonResponse)
    {
        var response = JsonSerializer.Deserialize<GeminiResponse>(jsonResponse, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (response == null || response.candidates == null || response.candidates.Count == 0)
            return string.Empty;

        var candidate = response.candidates[0];
        if (candidate.content == null || candidate.content.parts == null || candidate.content.parts.Count == 0)
            return string.Empty;

        return candidate.content.parts[0].text;
    }
}
