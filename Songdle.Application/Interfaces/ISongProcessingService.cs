using System;
using Songdle.Domain.Entities;

namespace Songdle.Application.Interfaces;

public interface ISongProcessingService
{
    
    Task<Song> GetSongByIdAsync(int id);
    Task<IEnumerable<Song>> GetAllSongsAsync();
}
