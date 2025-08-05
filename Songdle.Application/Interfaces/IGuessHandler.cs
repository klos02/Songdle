using System;
using Songdle.Application.DTOs;

namespace Songdle.Application.Interfaces;

public interface IGuessHandler
{
    public Task<AnswerCheckDto> GuessSong(AnswerDto answer, TodaysGameDto todaysGame);

}
