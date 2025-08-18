using System;
using Songdle.Application.DTOs;



namespace Songdle.Application.Interfaces;

public interface ITodaysGameProcessingService
{
    Task<TodaysGameDto?> GetTodaysGameAsync(DateTime date);
    Task SetTodaysGameAsync(DateTime date, string spotifySongId, string artist, string title);
    Task<bool> IsSongOfTheDaySetAsync(DateTime date);
    Task DeleteTodaysGameAsync(DateTime date);
    Task<IEnumerable<TodaysGameDto?>> GetGamesAsync(DateTime date);
}
