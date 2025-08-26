using System;
using System.Net.Http.Json;
using Songdle.Application.DTOs;
using Songdle.Application.Interfaces;
using Songdle.Infrastructure.Spotify;
using Songdle.Infrastructure.Spotify.Responses;

namespace Songdle.Infrastructure.Services;

public class AlbumHandler(SpotifyAuthService spotifyAuthService, HttpClient httpClient) : IAlbumHandler
{
    public Task<AlbumDto> GetAlbumByIdAsync(string albumId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<AlbumDto>> SearchAlbumsByNameAsync(string albumName)
    {
        var token = await spotifyAuthService.GetAccessTokenAsync();
        httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var url = $"https://api.spotify.com/v1/search?q={Uri.EscapeDataString(albumName)}&type=album&limit=10";
        var response = await httpClient.GetFromJsonAsync<SpotifyAlbumResponse>(url);

        if (response == null || response.albums == null)
            return [];


        return response.albums.Select(album =>
        {
            DateTime? releaseDate = null;
            if (!string.IsNullOrEmpty(album.release_date))
            {
                releaseDate = ParseReleaseDate(album.release_date);
            }

            return new AlbumDto
            {
                Id = album.id,
                Name = albumName,
                ReleaseDate = releaseDate ?? DateTime.MinValue,
                ImageUrl = album.images?.FirstOrDefault()?.url ?? string.Empty,
            };
        });


    }



    private static DateTime? ParseReleaseDate(string releaseDate)
    {
        if (string.IsNullOrEmpty(releaseDate))
            return null;

        // Spotify może zwrócić "YYYY", "YYYY-MM" lub "YYYY-MM-DD"
        string[] formats = ["yyyy", "yyyy-MM", "yyyy-MM-dd"];
        if (DateTime.TryParseExact(releaseDate, formats, null,
                                   System.Globalization.DateTimeStyles.None, out var parsedDate))
        {
            return parsedDate;
        }

        return null;
    }
}
