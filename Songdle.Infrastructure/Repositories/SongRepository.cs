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
        if(await SongExistsAsync(song))
        {
            throw new InvalidOperationException("Song already exists in the repository.");
        }
        else await context.Songs.AddAsync(song);
    }

    public async Task<IEnumerable<Song>> GetAllSongsAsync()
    {
        return await context.Songs.ToListAsync();
    }

    public async Task<Song?> GetSongByIdAsync(int id)
    {
        return await context.Songs.FirstOrDefaultAsync(s => s.Id == id);
    }
    public async Task DeleteSongAsync(int id)
    {
        var song = await GetSongByIdAsync(id);
        if (song != null)
        {
            context.Songs.Remove(song);
        }
    }

    public async Task<Song?> GetSongByTitleAsync(string title)
    {
        return await context.Songs.FirstOrDefaultAsync(s => s.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<IEnumerable<Song>> SearchSongsByTitleAsync(string partialTitle)
    {
        if (string.IsNullOrWhiteSpace(partialTitle))
        {
            throw new ArgumentException("Partial title cannot be null or empty.", nameof(partialTitle));
        }

        return await context.Songs
           .Where(s => s.Title.ToLower().Contains(partialTitle.ToLower()))
           .OrderBy(s => s.Title)
           .Take(10)
           .ToListAsync();

    }

    public Task<bool> SongExistsAsync(Song song)
    {
        if (song == null)
        {
            throw new ArgumentNullException(nameof(song), "Song cannot be null.");
        }

        return context.Songs.AnyAsync(s => s.Title.Equals(song.Title, StringComparison.OrdinalIgnoreCase) && s.Artist.Equals(song.Artist, StringComparison.OrdinalIgnoreCase));
    }
}
