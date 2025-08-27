using System;
using Microsoft.AspNetCore.Components;
using Songdle.Application.DTOs;
using Songdle.Application.Interfaces;
using Songdle.Domain.Entities;
using Songdle.Infrastructure.Spotify.Responses;

namespace Songdle.Presentation.Components.Pages;

public partial class SongdleAlbumComponent(ITodaysAlbumGameProcessingService todaysAlbumGameProcessingService, IAlbumProcessingService albumProcessingService) : ComponentBase
{
    private string guessInput = "";
    private bool isLoading = false;
    private bool isCorrect = false;
    private TodaysAlbumGameDto? todaysAlbumGame = null;

    private List<AlbumDto> suggestions = [];
    private List<AlbumDto> guessResults = [];
    private AlbumDto? selectedAlbum = null;
    
    private HashSet<int> revealedTiles = new();

    private bool isAlbumSelected => selectedAlbum != null;

    protected override async Task OnInitializedAsync()
    {
        await GetTodaysAlbumGame();
    }

    private async Task GetTodaysAlbumGame()
    {
        try
        {
            isLoading = true;
            todaysAlbumGame = await todaysAlbumGameProcessingService.GetTodaysAlbumGameAsync(DateTime.UtcNow.Date);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading today's album game: {ex.Message}");
        }
        finally
        {
            isLoading = false;
        }
    }

    private async Task OnInputChanged(ChangeEventArgs e)
    {
        guessInput = e.Value?.ToString() ?? "";
        selectedAlbum = null;

        if (guessInput.Length >= 2)
        {
            suggestions = (await albumProcessingService.SearchAlbumsByNameAsync(guessInput)).ToList();
        }
        else
        {
            suggestions.Clear();
        }
    }

    private void SelectSuggestion(AlbumDto album)
    {
        selectedAlbum = album;
        guessInput = album.Name;
        suggestions.Clear();
    }

    private async Task SubmitGuess()
    {
        if (selectedAlbum is not null)
        {

            guessResults.Insert(0, selectedAlbum);

            isCorrect = selectedAlbum.Id == todaysAlbumGame?.SpotifyAlbumId;

            if (!isCorrect)
            {
                RevealNextTile();
            }


            Console.WriteLine($"Guessed album: {selectedAlbum.Name} - Correct: {isCorrect}");
            Console.WriteLine($"Today's song: {todaysAlbumGame.AlbumName}");
        }

        guessInput = "";
        selectedAlbum = null;
    }

    void RevealNextTile()
    {
        var hidden = Enumerable.Range(0, 16).Where(i => !revealedTiles.Contains(i)).ToList();
        if (hidden.Count != 0)
        {
            var random = new Random();
            int index = hidden[random.Next(hidden.Count)];
            revealedTiles.Add(index);
        }
    }
    
    private string GetBoolClass(bool isCorrect) => isCorrect ? "correct" : "incorrect";
}
