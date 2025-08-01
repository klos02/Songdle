using System;
using Songdle.Domain.Entities;

namespace Songdle.Application.Interfaces;

public interface ISongHandler
{
    Task<Song?> GetSongByIdAsync(int id);
    Task<IEnumerable<Song>> GetAllSongsAsync();
    Task AddSongAsync(Song song);
    Task DeleteSongAsync(int id);
}
