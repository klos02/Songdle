using System;
using Songdle.Application.Interfaces;
using Songdle.Domain.Entities;
using Songdle.Domain.Interfaces;

namespace Songdle.Infrastructure.Services;

public class TodaysAlbumGameHandler(ITodaysAlbumGameRepository todaysAlbumGameRepository, IUnitOfWork unitOfWork, IAlbumHandler albumHandler) : ITodaysAlbumGameHandler
{
    public async Task DeleteTodaysAlbumGameAsync(DateTime date)
    {
        await todaysAlbumGameRepository.DeleteTodaysAlbumGameAsync(date);
        await unitOfWork.SaveChangesAsync();

    }

    public async Task<IEnumerable<TodaysAlbumGame?>> GetAlbumGamesAsync(DateTime date)
    {
        return await todaysAlbumGameRepository.GetAlbumGamesAsync(date);
    }

    public async Task<TodaysAlbumGame?> GetTodaysAlbumGameAsync(DateTime date)
    {
        return await todaysAlbumGameRepository.GetTodaysAlbumGameAsync(date);
    }

    public async Task<bool> IsAlbumOfTheDaySetAsync(DateTime date)
    {
        return await todaysAlbumGameRepository.GetTodaysAlbumGameAsync(date) is not null;
    }

    public async Task SetTodaysAlbumGameAsync(DateTime date, TodaysAlbumGame todaysAlbumGame)
    {
        var album = await albumHandler.GetAlbumByIdAsync(todaysAlbumGame.SpotifyAlbumId) ?? throw new ArgumentException($"Album with ID {todaysAlbumGame.SpotifyAlbumId} not found.");

        todaysAlbumGame.AlbumName = album.Name;
        todaysAlbumGame.AlbumImageUrl = album.ImageUrl;

        await todaysAlbumGameRepository.SetTodaysAlbumGameAsync(todaysAlbumGame);
        await unitOfWork.SaveChangesAsync();
    }
}
