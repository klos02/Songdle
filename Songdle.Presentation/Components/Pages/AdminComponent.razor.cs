using Microsoft.AspNetCore.Components;
using Songdle.Application.DTOs;
using Songdle.Application.Interfaces;

namespace Songdle.Presentation.Components.Pages;

public partial class AdminComponent(ISongProcessingService SongProcessingService, ITodaysGameProcessingService TodaysGameProcessingService) : ComponentBase
{
    
    private IEnumerable<SongDto> songs = new List<SongDto>();
    private IEnumerable<TodaysGameDto?> todaysGames = new List<TodaysGameDto?>();
    private bool isLoading = false;
    

    private string searchQuery = "";
    private DateTime selectedDate = DateTime.UtcNow.Date;


    protected override async Task OnInitializedAsync()
    {
        await GetGames();
    }


    private async Task SearchSongs()
    {
        if (string.IsNullOrWhiteSpace(searchQuery))
        {
            return;
        }

        try
        {
            isLoading = true;
            songs = await SongProcessingService.SearchSongsByTitleAsync(searchQuery);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error searching songs: {ex.Message}");
            songs = new List<SongDto>();
        }
        finally
        {
            isLoading = false;
        }
    }

    private async Task<bool> IsTodaysGameSet()
    {
        return await TodaysGameProcessingService.IsSongOfTheDaySetAsync(selectedDate);
    }

    private async Task SetTodaysGame(string songId)
    {
        try
        {
            isLoading = true;
            if (await IsTodaysGameSet())
            {
                await TodaysGameProcessingService.DeleteTodaysGameAsync(selectedDate);
            }
            await TodaysGameProcessingService.SetTodaysGameAsync(selectedDate, songId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error setting today's game: {ex.Message}");
        }
        finally
        {
            isLoading = false;
        }
    }

    private async Task GetGames(){
        todaysGames = await  TodaysGameProcessingService.GetGamesAsync(DateTime.UtcNow.Date);
    }


}

