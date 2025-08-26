using System;
using AutoMapper;
using Songdle.Application.DTOs;
using Songdle.Application.Interfaces;
using Songdle.Domain.Entities;

namespace Songdle.Application.Services;

public class TodaysAlbumGameProcessingService(ITodaysAlbumGameHandler todaysAlbumGameHandler, IMapper mapper) : ITodaysAlbumGameProcessingService
{
    public async Task DeleteTodaysAlbumGameAsync(DateTime date)
    {
        await todaysAlbumGameHandler.DeleteTodaysAlbumGameAsync(date);
    }

    public async Task<IEnumerable<TodaysAlbumGameDto?>> GetAlbumGamesAsync(DateTime date)
    {
        var albumGames = await todaysAlbumGameHandler.GetAlbumGamesAsync(date);
        return mapper.Map<IEnumerable<TodaysAlbumGameDto?>>(albumGames);
    }

    public async Task<TodaysAlbumGameDto?> GetTodaysAlbumGameAsync(DateTime date)
    {
        var albumGame = await todaysAlbumGameHandler.GetTodaysAlbumGameAsync(date);

        return albumGame != null
            ? mapper.Map<TodaysAlbumGameDto>(albumGame)
            : null;

    }

    public async Task<bool> IsAlbumOfTheDaySetAsync(DateTime date)
    {
        return await todaysAlbumGameHandler.IsAlbumOfTheDaySetAsync(date);
    }

    public async Task SetTodaysAlbumGameAsync(DateTime date, string spotifyAlbumId)
    {
        await todaysAlbumGameHandler.SetTodaysAlbumGameAsync(date, new TodaysAlbumGame
        {
            Date = date,
            SpotifyAlbumId = spotifyAlbumId
        });
    }
}
