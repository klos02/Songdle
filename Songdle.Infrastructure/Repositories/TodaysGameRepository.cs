using System;
using Microsoft.EntityFrameworkCore;
using Songdle.Domain.Entities;
using Songdle.Domain.Interfaces;
using Songdle.Infrastructure.Data;

namespace Songdle.Infrastructure.Repositories;

public class TodaysGameRepository(AppDbContext context) : ITodaysGameRepository
{
    public async Task<TodaysGame?> GetTodaysGameAsync(DateTime date)
    {
        return await context.Games.Include(g => g.SongOfTheDay)
            .FirstOrDefaultAsync(g => g.Date.Date == date.Date);
    }

    public async Task SetTodaysGameAsync(DateTime date, TodaysGame todaysGame)
    {
        await context.Games.AddAsync(todaysGame);
    }
}
