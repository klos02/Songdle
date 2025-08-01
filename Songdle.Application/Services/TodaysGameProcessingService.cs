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

        return game != null
            ? new TodaysGameDto
            {
                Id = game.Id,
                Date = game.Date,
                SongOfTheDayId = game.SongOfTheDayId,
                SongOfTheDay = game.SongOfTheDay != null ? new SongDto
                {
                    Id = game.SongOfTheDay.Id,
                    Title = game.SongOfTheDay.Title,
                    Artist = game.SongOfTheDay.Artist,
                    Album = game.SongOfTheDay.Album,
                    ReleaseDate = game.SongOfTheDay.ReleaseDate,
                    Genre = game.SongOfTheDay.Genre,
                    LyricsFragment = game.SongOfTheDay.LyricsFragment,
                    Country = game.SongOfTheDay.Country,
                    ImageUrl = game.SongOfTheDay.ImageUrl,
                    AudioPreviewUrl = game.SongOfTheDay.AudioPreviewUrl,
                    Feats = game.SongOfTheDay.Feats ?? []


                } : null
            }
            : null;
    }

    public async Task<bool> IsSongOfTheDaySetAsync(DateTime date)
    {

        return await todaysGameHandler.IsSongOfTheDaySetAsync(date);
    }

    public async Task SetTodaysGameAsync(DateTime date, int songOfTheDayId)
    {
        var songOfTheDay = await songHandler.GetSongByIdAsync(songOfTheDayId);
        var game = new TodaysGame
        {
            Date = date,
            SongOfTheDayId = songOfTheDayId,
            SongOfTheDay = songOfTheDay

        };
        await todaysGameHandler.SetTodaysGame(date, game);
    }

}
