using System;
using System.Net.Http.Json;
using System.Text.Json;
using Songdle.Domain.Entities;
using Songdle.Domain.Interfaces;
using Songdle.Infrastructure.Spotify;
using Songdle.Infrastructure.Spotify.Responses;

namespace Songdle.Infrastructure.Repositories;

public class SpotifySongRepository(SpotifyAuthService spotifyAuthService, HttpClient httpClient) : ISongRepository
{
    public Task AddSongAsync(Song song)
    {
        throw new NotImplementedException();
    }

    public Task DeleteSongAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Song>> GetAllSongsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Song?> GetSongByIdAsync(string id)
    {
        var token = await spotifyAuthService.GetAccessTokenAsync();
        httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.GetAsync($"https://api.spotify.com/v1/tracks/{id}");
        if (!response.IsSuccessStatusCode)
            return null;

        var json = await response.Content.ReadAsStringAsync();

        //Console.WriteLine(json);

        var trackData = JsonSerializer.Deserialize<TrackItem>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = false
        });

        if (trackData == null)
            return null;

        var track = trackData;

        var mainArtist = track.artists.FirstOrDefault()?.name ?? "Unknown Artist";
        //Console.WriteLine($"Pobrano piosenkę: {track.name} - Artysta: {mainArtist} (ID: {track.id})");
        var feats = track.artists.Count > 1 ? track.artists.Skip(1).Select(a => a.name).ToList() : new List<string>();

        DateTime? releaseDate = null;
        if (!string.IsNullOrEmpty(track.album.release_date))
        {
            // Spotify może zwrócić "YYYY", "YYYY-MM" lub "YYYY-MM-DD"
            string[] formats = { "yyyy", "yyyy-MM", "yyyy-MM-dd" };
            if (DateTime.TryParseExact(track.album.release_date, formats, null,
                                       System.Globalization.DateTimeStyles.None, out var parsedDate))
            {
                releaseDate = parsedDate;
            }
        }
        return new Song
        {
            SpotifyId = track.id,
            Title = track.name,
            Artist = mainArtist,
            Feats = feats,
            Album = track.album.name,
            ReleaseDate = releaseDate ?? DateTime.Now,
            AudioPreviewUrl = track.preview_url,
            ImageUrl = track.album.images?.FirstOrDefault()?.url,
            Popularity = track.popularity,
            Duration = track.duration_ms
        };
    }

    public Task<Song?> GetSongByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Song?> GetSongByTitleAsync(string title)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Song>> SearchSongsByTitleAsync(string partialTitle)
    {
        var token = await spotifyAuthService.GetAccessTokenAsync();
        httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var url = $"https://api.spotify.com/v1/search?q={Uri.EscapeDataString(partialTitle)}&type=track&limit=10";
        var response = await httpClient.GetFromJsonAsync<SpotifySearchResponse>(url);

        if (response?.tracks?.items == null)
            return [];

        return response.tracks.items.GroupBy(t => new { Title = t.name, Artist = t.artists.FirstOrDefault()?.name ?? "Unknown Artist" })
        .Select(g => g.OrderByDescending(t => t.album.album_type == "album").First())
        .Select(t =>
        {
            var mainArtist = t.artists.FirstOrDefault()?.name ?? "Unknown Artist";
            var feats = t.artists.Count > 1 ? t.artists?.Skip(1).Select(a => a.name).ToList() : new List<string>();

            DateTime? releaseDate = null;
            if (!string.IsNullOrEmpty(t.album.release_date))
            {
                // Spotify może zwrócić "YYYY", "YYYY-MM" lub "YYYY-MM-DD"
                string[] formats = { "yyyy", "yyyy-MM", "yyyy-MM-dd" };
                if (DateTime.TryParseExact(t.album.release_date, formats, null,
                                           System.Globalization.DateTimeStyles.None, out var parsedDate))
                {
                    releaseDate = parsedDate;
                }
            }

            Console.WriteLine($"Found song: {t.name} by {mainArtist} (ID: {t.id})");

            return new Song
            {
                SpotifyId = t.id,
                Title = t.name,
                Artist = mainArtist,
                Feats = feats,
                Album = t.album.name,
                ReleaseDate = releaseDate ?? DateTime.Now,
                AudioPreviewUrl = t.preview_url,
                ImageUrl = t.album.images?.FirstOrDefault()?.url,
                Popularity = t.popularity,
                Duration = t.duration_ms

            };

        }

        );
    }

    public Task<bool> SongExistsAsync(Song song)
    {
        throw new NotImplementedException();
    }
}
