using System;
using Songdle.Domain.Entities;

namespace Songdle.Domain.Interfaces;

public interface ISongRepository
{
    Task<Song?> GetSongByIdAsync(int id);
    Task<IEnumerable<Song>> GetAllSongsAsync();
    Task AddSongAsync(Song song);
    Task DeleteSongAsync(int id);
    Task<Song?> GetSongByTitleAsync(string title);
    Task<IEnumerable<Song>> SearchSongsByTitleAsync(string partialTitle);
    Task<bool> SongExistsAsync(Song song);
}
