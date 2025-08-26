using System;

using Songdle.Domain.Entities;

namespace Songdle.Application.Interfaces;

public interface ITodaysAlbumGameHandler
{
    Task<TodaysAlbumGame?> GetTodaysAlbumGameAsync(DateTime date);
    Task SetTodaysAlbumGameAsync(DateTime date, TodaysAlbumGame todaysAlbumGame);
    Task DeleteTodaysAlbumGameAsync(DateTime date);
    Task<IEnumerable<TodaysAlbumGame?>> GetAlbumGamesAsync(DateTime date);
    Task<bool> IsAlbumOfTheDaySetAsync(DateTime date);
}
