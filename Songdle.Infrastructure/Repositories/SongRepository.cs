using System;
using Microsoft.EntityFrameworkCore;
using Songdle.Domain.Entities;
using Songdle.Domain.Interfaces;
using Songdle.Infrastructure.Data;

namespace Songdle.Infrastructure.Repositories;

public class SongRepository(AppDbContext context) : ISongRepository
{
    public async Task AddSongAsync(Song song)
    {
        await context.Songs.AddAsync(song);
    }

    public async Task<IEnumerable<Song>> GetAllSongsAsync()
    {
        return await context.Songs.ToListAsync();
    }

    public async Task<Song?> GetSongByIdAsync(int id)
    {
        return await context.Songs.FirstOrDefaultAsync(s => s.Id == id);
    }
}
