using System;
using Songdle.Application.DTOs;

namespace Songdle.Application.Interfaces;

public interface ITodaysAlbumGameProcessingService
{
    Task<TodaysAlbumGameDto?> GetTodaysAlbumGameAsync(DateTime date);
    Task SetTodaysAlbumGameAsync(DateTime date, string spotifyAlbumId);
    Task<bool> IsAlbumOfTheDaySetAsync(DateTime date);
    Task DeleteTodaysAlbumGameAsync(DateTime date);
    Task<IEnumerable<TodaysAlbumGameDto?>> GetAlbumGamesAsync(DateTime date);
}
