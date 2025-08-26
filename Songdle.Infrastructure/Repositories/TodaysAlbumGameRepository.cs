using System;
using Microsoft.EntityFrameworkCore;
using Songdle.Domain.Entities;
using Songdle.Domain.Interfaces;
using Songdle.Infrastructure.Data;

namespace Songdle.Infrastructure.Repositories;

public class TodaysAlbumGameRepository(AppDbContext context) : ITodaysAlbumGameRepository
{
    public async Task DeleteTodaysAlbumGameAsync(DateTime date)
    {
        await context.AlbumGames
        .Where(g => g.Date.Date == date.Date).ExecuteDeleteAsync();
    }

    public async Task<IEnumerable<TodaysAlbumGame?>> GetAlbumGamesAsync(DateTime date)
    {
        return await context.AlbumGames
            .Where(g => g.Date.Date >= date.Date.AddDays(-1) && g.Date.Date <= date.Date.AddDays(6))
            .ToListAsync();
    }

    public async Task<TodaysAlbumGame?> GetTodaysAlbumGameAsync(DateTime date)
    {
        return await context.AlbumGames.FirstOrDefaultAsync(g => g.Date.Date == date.Date);
    }

    public async Task SetTodaysAlbumGameAsync(TodaysAlbumGame todaysAlbumGame)
    {
        await context.AlbumGames.AddAsync(todaysAlbumGame);
    }
}
