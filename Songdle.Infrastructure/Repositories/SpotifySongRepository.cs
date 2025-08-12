using System;
using System.Net.Http.Json;
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

        return response.tracks.items.Select(t =>
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

            return new Song
            {
                SpotifyId = t.id,
                Title = t.name,
                Artist = mainArtist,
                Feats = feats,
                Album = t.album.name,
                ReleaseDate = releaseDate ?? DateTime.Now,
                AudioPreviewUrl = t.preview_url,
                ImageUrl = t.album.images?.FirstOrDefault()?.url
            };

        }

        );
    }

    public Task<bool> SongExistsAsync(Song song)
    {
        throw new NotImplementedException();
    }
}
