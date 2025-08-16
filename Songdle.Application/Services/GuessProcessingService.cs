using System;
using Songdle.Application.DTOs;
using Songdle.Application.Interfaces;

namespace Songdle.Application.Services;

public class GuessProcessingService(IGuessHandler guessHandler) : IGuessProcessingService
{
    public Task<AnswerDto> GetAnswerDto(SongDto song)
    {
        return Task.FromResult(new AnswerDto
        {
            Title = song.Title,
            Artist = song.Artist,
            Album = song.Album,
            ReleaseDate = song.ReleaseDate,
            Feats = song.Feats ?? [],
            Popularity = song.Popularity,

        });
    }

    public Task<AnswerCheckDto> GuessSong(AnswerDto answer, TodaysGameDto todaysGame)
    {
        return guessHandler.GuessSong(answer, todaysGame);
    }
}
