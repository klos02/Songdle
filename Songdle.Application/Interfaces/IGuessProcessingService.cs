using System;
using Songdle.Application.DTOs;

namespace Songdle.Application.Interfaces;

public interface IGuessProcessingService
{
    public Task<AnswerCheckDto> GuessSong(AnswerDto answer, TodaysGameDto todaysGame);
}
