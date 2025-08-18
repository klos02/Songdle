using System;
using Songdle.Application.Interfaces;
using Songdle.Application.Prompts;
using Songdle.Domain.Entities;
using Songdle.Domain.Interfaces;
using Songdle.Infrastructure.AIClients;

namespace Songdle.Infrastructure.Services;

public class TodaysGameHandler(ITodaysGameRepository todaysGameRepository, IUnitOfWork unitOfWork, GeminiClient geminiClient) : ITodaysGameHandler
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
        return todaysGame?.SpotifySongId != null;

    }


    public async Task SetTodaysGame(DateTime date, TodaysGame todaysGame, string artist, string title)
    {
        todaysGame.Clue = await GetClueForSongAsync(artist, title);

    
        await todaysGameRepository.SetTodaysGameAsync(date, todaysGame);
        await unitOfWork.SaveChangesAsync();

    }

    private async Task<string> GetClueForSongAsync(string artist, string title)
    {
        var prompt = SongCluePrompt.GetPrompt() + $"\nArtist: {artist}\nTitle: {title}\n";

        Console.WriteLine($"Prompt for AI: {prompt}");

        var response = await geminiClient.GetResponseAsync(prompt) ?? throw new InvalidOperationException("Failed to generate clue for the song.");
        return geminiClient.GetResponseText(response);
        
    }
}
