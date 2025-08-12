using System;
using Songdle.Application.DTOs;
using Songdle.Domain.Entities;

namespace Songdle.Application.Interfaces;

public interface ISongHandler
{
    Task<Song?> GetSongByIdAsync(string id);
    Task<IEnumerable<Song>> GetAllSongsAsync();
    Task<Song?> GetSongByTitleAsync(string title);
    Task<IEnumerable<Song>> SearchSongsByTitleAsync(string partialTitle);
    Task AddSongAsync(Song song);
    Task AddSongFromJsonAsync(string json);
    Task DeleteSongAsync(int id);
}
