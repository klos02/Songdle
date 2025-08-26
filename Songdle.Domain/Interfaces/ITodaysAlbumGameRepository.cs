using System;
using Songdle.Domain.Entities;

namespace Songdle.Domain.Interfaces;

public interface ITodaysAlbumGameRepository
{
    Task<TodaysAlbumGame?> GetTodaysAlbumGameAsync(DateTime date);
    Task SetTodaysAlbumGameAsync(TodaysAlbumGame todaysAlbumGame);
    Task DeleteTodaysAlbumGameAsync(DateTime date);
    Task<IEnumerable<TodaysAlbumGame?>> GetAlbumGamesAsync(DateTime date);
}
