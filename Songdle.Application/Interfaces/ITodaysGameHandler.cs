using System;
using Songdle.Domain.Entities;

namespace Songdle.Application.Interfaces;

public interface ITodaysGameHandler
{
    public Task<TodaysGame> GetTodaysGame(DateTime date);
    public Task SetTodaysGame(DateTime date, TodaysGame todaysGame);
    public Task<bool> IsSongOfTheDaySetAsync(DateTime date);
}
