using System;
using Songdle.Domain.Entities;

namespace Songdle.Domain.Interfaces;

public interface ISongRepository
{
    Task<Song?> GetSongByIdAsync(int id);
    Task<IEnumerable<Song>> GetAllSongsAsync();
    Task AddSongAsync(Song song);
}
