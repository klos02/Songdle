using System;
using Songdle.Application.DTOs;
using Songdle.Domain.Entities;


namespace Songdle.Application.Interfaces;

public interface ITodaysGameProcessingService
{
    Task<TodaysGameDto?> GetTodaysGameAsync(DateTime date);
    Task SetTodaysGameAsync(DateTime date, int songOfTheDayId);
    Task<bool> IsSongOfTheDaySetAsync(DateTime date);
}
