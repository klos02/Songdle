using System;
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

    public async Task DeleteSongAsync(int id)
    {
        await songRepository.DeleteSongAsync(id);
    }

    public async Task<IEnumerable<Song>> GetAllSongsAsync()
    {
        return await songRepository.GetAllSongsAsync();
    }

    public Task<Song?> GetSongByIdAsync(int id)
    {
        return songRepository.GetSongByIdAsync(id) 
            ?? throw new KeyNotFoundException($"Song with ID {id} not found.");
    }
}
