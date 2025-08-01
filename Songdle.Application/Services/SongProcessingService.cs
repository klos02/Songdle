using System;
using Songdle.Application.Interfaces;
using Songdle.Domain.Entities;

namespace Songdle.Application.Services;

public class SongProcessingService : ISongProcessingService
{
    public async Task<IEnumerable<Song>> GetAllSongsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Song> GetSongByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

}
