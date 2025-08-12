using System;
using System.Text.Json;
using Songdle.Application.DTOs;
using Songdle.Application.Interfaces;
using Songdle.Domain.Entities;
using Songdle.Domain.Interfaces;

namespace Songdle.Infrastructure.Services;

public class SongHandler(ISongRepository songRepository, IUnitOfWork unitOfWork) : ISongHandler
{
    public async Task AddSongAsync(Song song)
    {
        ArgumentNullException.ThrowIfNull(song);

        await songRepository.AddSongAsync(song);

        await unitOfWork.SaveChangesAsync();

    }

    public async Task AddSongFromJsonAsync(string json)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    
        foreach (var song in JsonSerializer.Deserialize<IEnumerable<Song>>(json, options) ?? [])
        {
            if (song != null)
            {
                await AddSongAsync(song);
            }
        }
    }

    public async Task DeleteSongAsync(int id)
    {
        await songRepository.DeleteSongAsync(id);
    }

    public async Task<IEnumerable<Song>> GetAllSongsAsync()
    {
        return await songRepository.GetAllSongsAsync();
    }

    public Task<Song?> GetSongByIdAsync(string id)
    {
        return songRepository.GetSongByIdAsync(id)
            ?? throw new KeyNotFoundException($"Song with ID {id} not found.");
    }

    public async Task<Song?> GetSongByTitleAsync(string title)
    {
        ArgumentNullException.ThrowIfNull(title);

        return await songRepository.GetSongByTitleAsync(title);
    }

    public async Task<IEnumerable<Song>> SearchSongsByTitleAsync(string partialTitle)
    {
        return await songRepository.SearchSongsByTitleAsync(partialTitle);
    }
}
