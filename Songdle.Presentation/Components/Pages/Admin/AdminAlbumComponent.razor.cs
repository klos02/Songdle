using System;
using Microsoft.AspNetCore.Components;
using Songdle.Application.DTOs;
using Songdle.Application.Interfaces;

namespace Songdle.Presentation.Components.Pages.Admin;

public partial class AdminAlbumComponent(IAlbumProcessingService albumProcessingService, ITodaysAlbumGameProcessingService todaysAlbumGameProcessingService) : ComponentBase
{
    private DateTime selectedDate = DateTime.UtcNow.Date;
    private bool isLoading = false;
    private string searchQuery = "";

    private IEnumerable<AlbumDto> albums = [];
    private IEnumerable<TodaysAlbumGameDto?> games = [];

    protected override async Task OnInitializedAsync()
    {
        await GetGames();
    }


    private async Task SearchAlbums()
    {
        if (string.IsNullOrWhiteSpace(searchQuery))
        {
            return;
        }
        try
        {
            isLoading = true;
            albums = await albumProcessingService.SearchAlbumsByNameAsync(searchQuery);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error searching albums: {ex.Message}");
            albums = [];
        }
        finally
        {
            isLoading = false;
        }
    }

    private async Task<bool> IsTodaysAlbumGameSet()
    {
        return await todaysAlbumGameProcessingService.IsAlbumOfTheDaySetAsync(selectedDate);
    }


    private async Task SetTodaysAlbumGame(AlbumDto album)
    {
        try
        {
            isLoading = true;
            if (await IsTodaysAlbumGameSet())
            {
                await todaysAlbumGameProcessingService.DeleteTodaysAlbumGameAsync(selectedDate);
            }

            await todaysAlbumGameProcessingService.SetTodaysAlbumGameAsync(selectedDate, album.Id);
            await GetGames();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error setting today's album game: {ex.Message}");
        }
        finally
        {
            isLoading = false;
        }
    }





    private async Task GetGames()
    {
        try
        {
            isLoading = true;
            games = await todaysAlbumGameProcessingService.GetAlbumGamesAsync(DateTime.UtcNow.Date);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching today's album games: {ex.Message}");
            games = [];
        }
        finally
        {
            isLoading = false;
        }
    }
}
