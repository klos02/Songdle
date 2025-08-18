using System;
using AutoMapper;
using Songdle.Application.DTOs;
using Songdle.Application.Interfaces;
using Songdle.Domain.Entities;

namespace Songdle.Application.Services;

public class TodaysGameProcessingService(ITodaysGameHandler todaysGameHandler, ISongHandler songHandler, IMapper mapper) : ITodaysGameProcessingService
{
    public async Task DeleteTodaysGameAsync(DateTime date)
    {
        await todaysGameHandler.DeleteTodaysGameAsync(date);
    }

    public async Task<IEnumerable<TodaysGameDto?>> GetGamesAsync(DateTime date)
    {
        var games = await todaysGameHandler.GetGamesAsync(date);
        // return games.Select(game => new TodaysGameDto
        // {
        //     SpotifySongId = game.SpotifySongId,
        //     Date = game.Date
        // });
        return mapper.Map<IEnumerable<TodaysGameDto?>>(games);
    }

    public async Task<TodaysGameDto?> GetTodaysGameAsync(DateTime date)
    {
        var game = await todaysGameHandler.GetTodaysGame(date);
        var songOfTheDay = songHandler.GetSongByIdAsync(game?.SpotifySongId);

        return game != null
            ?
            mapper.Map<TodaysGameDto>(game) : null;
        // new TodaysGameDto
        // {
        //     Id = game.Id,
        //     Date = game.Date,
        //     SpotifySongId = game.SpotifySongId,
        // }
        // : null;
    }

    public async Task<bool> IsSongOfTheDaySetAsync(DateTime date)
    {

        return await todaysGameHandler.IsSongOfTheDaySetAsync(date);
    }

    public async Task SetTodaysGameAsync(DateTime date, string spoifySongId, string artist, string title)
    {
        //var songOfTheDay = await songHandler.GetSongByIdAsync(spoifySongId);
        var game = new TodaysGame
        {
            Date = date,
            SpotifySongId = spoifySongId,

        };
        await todaysGameHandler.SetTodaysGame(date, game, artist, title);
    }

}
