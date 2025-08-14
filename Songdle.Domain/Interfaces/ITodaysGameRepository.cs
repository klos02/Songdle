using System;
using Songdle.Domain.Entities;

namespace Songdle.Domain.Interfaces;

public interface ITodaysGameRepository
{
    Task<TodaysGame?> GetTodaysGameAsync(DateTime date);
    Task SetTodaysGameAsync(DateTime date, TodaysGame todaysGame);
    Task DeleteTodaysGameAsync(DateTime date);
    Task<IEnumerable<TodaysGame?>> GetGamesAsync(DateTime date);
}
