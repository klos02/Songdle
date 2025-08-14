using System;
using Songdle.Application.Interfaces;
using Songdle.Domain.Entities;
using Songdle.Domain.Interfaces;

namespace Songdle.Infrastructure.Services;

public class TodaysGameHandler(ITodaysGameRepository todaysGameRepository, IUnitOfWork unitOfWork) : ITodaysGameHandler
{
    public async Task DeleteTodaysGameAsync(DateTime date)
    {
        await todaysGameRepository.DeleteTodaysGameAsync(date);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<TodaysGame?>> GetGamesAsync(DateTime date)
    {
        return await todaysGameRepository.GetGamesAsync(date);
    }

    public async Task<TodaysGame> GetTodaysGame(DateTime date)
    {
        return await todaysGameRepository.GetTodaysGameAsync(date)
            ?? throw new KeyNotFoundException($"Today's game for date {date.ToShortDateString()} not found.");
    }

    public async Task<bool> IsSongOfTheDaySetAsync(DateTime date)
    {
        var todaysGame = await todaysGameRepository.GetTodaysGameAsync(date);
        return todaysGame.SpotifySongId != null;
        
    }


    public async Task SetTodaysGame(DateTime date, TodaysGame todaysGame)
    {
        await todaysGameRepository.SetTodaysGameAsync(date, todaysGame);
        await unitOfWork.SaveChangesAsync();
           
    }
}
