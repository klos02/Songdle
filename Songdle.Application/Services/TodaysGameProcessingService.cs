using System;
using Songdle.Application.DTOs;
using Songdle.Application.Interfaces;
using Songdle.Domain.Entities;

namespace Songdle.Application.Services;

public class TodaysGameProcessingService(ITodaysGameHandler todaysGameHandler, ISongHandler songHandler) : ITodaysGameProcessingService
{
    public async Task<TodaysGameDto?> GetTodaysGameAsync(DateTime date)
    {
        var game = await todaysGameHandler.GetTodaysGame(date);
        var songOfTheDay = songHandler.GetSongByIdAsync(game?.SpotifySongId);

        return game != null
            ? new TodaysGameDto
            {
                Id = game.Id,
                Date = game.Date,
                SpotifySongId = game.SpotifySongId,
            }
            : null;
    }

    public async Task<bool> IsSongOfTheDaySetAsync(DateTime date)
    {

        return await todaysGameHandler.IsSongOfTheDaySetAsync(date);
    }

    public async Task SetTodaysGameAsync(DateTime date, string spoifySongId)
    {
        //var songOfTheDay = await songHandler.GetSongByIdAsync(spoifySongId);
        var game = new TodaysGame
        {
            Date = date,
            SpotifySongId = spoifySongId,

        };
        await todaysGameHandler.SetTodaysGame(date, game);
    }

}
