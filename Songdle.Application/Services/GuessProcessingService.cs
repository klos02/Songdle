using System;
using Songdle.Application.DTOs;
using Songdle.Application.Interfaces;

namespace Songdle.Application.Services;

public class GuessProcessingService(IGuessHandler guessHandler) : IGuessProcessingService
{
    public Task<AnswerCheckDto> GuessSong(AnswerDto answer, TodaysGameDto todaysGame)
    {
        return guessHandler.GuessSong(answer, todaysGame);
    }
}
