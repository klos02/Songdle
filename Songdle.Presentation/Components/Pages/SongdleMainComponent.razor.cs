using Microsoft.AspNetCore.Components;
using Songdle.Application.DTOs;
using Songdle.Application.Interfaces;


namespace Songdle.Presentation.Components.Pages;

public partial class SongdleMainComponent(ISongProcessingService SongProcessingService, ITodaysGameProcessingService TodaysGameProcessingService, IGuessProcessingService GuessProcessingService, NavigationManager navigationManager) : ComponentBase
{

    private string guessInput = "";
    private SongDto? selectedSong = null;
    private List<SongDto> suggestions = new();
    private List<SongDto> guessResults = new();
    private List<AnswerCheckDto> answerResults = new();
    private TodaysGameDto todaysGame;
    private string? previewUrl = "";
    private bool isCorrect = false;
    private bool isLoadingGame = false;

    private bool isSongSelected => selectedSong != null;

    protected override async Task OnInitializedAsync()
    {
        await GetTodaysGame();
    }

    private async Task GetTodaysGame()
    {
        try
        {
            isLoadingGame = true;
            todaysGame = await TodaysGameProcessingService.GetTodaysGameAsync(DateTime.UtcNow.Date);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading today's game: {ex.Message}");
        }
        finally
        {
            isLoadingGame = false;
        }
    }

    private async Task OnInputChanged(ChangeEventArgs e)
    {
        guessInput = e.Value?.ToString() ?? "";
        selectedSong = null;

        if (guessInput.Length >= 2)
        {
            suggestions = (await SongProcessingService.SearchSongsByTitleAsync(guessInput)).ToList();
        }
        else
        {
            suggestions.Clear();
        }
    }

    private void SelectSuggestion(SongDto song)
    {
        selectedSong = song;
        guessInput = song.Title;
        suggestions.Clear();
    }

    private async Task SubmitGuess()
    {
        if (selectedSong is not null)
        {
            var answerDto = await GuessProcessingService.GetAnswerDto(selectedSong);
            var answerCheck = await GuessProcessingService.GuessSong(answerDto, todaysGame);

            guessResults.Insert(0, selectedSong);
            answerResults.Insert(0, answerCheck);

            isCorrect = selectedSong.SpotifyId == todaysGame.SpotifySongId;






            if (isCorrect)
            {
                previewUrl = selectedSong.AudioPreviewUrl;
            }
            Console.WriteLine($"Guessed song: {selectedSong.Title} - Correct: {isCorrect}");
            Console.WriteLine($"Today's song: {todaysGame.SpotifySongId}");
        }

        guessInput = "";
        selectedSong = null;
    }

    // Styling helper methods
    private string GetAccuracyClass(int check) => check switch
    {
        1 => "correct",
        2 => "partial",
        _ => "incorrect"
    };

    private string GetBoolClass(bool isCorrect) => isCorrect ? "correct" : "incorrect";

    private string GetIntClass(int check) => check switch
    {
        1 => "correct",
        0 or 2 => "incorrect",
        _ => "partial"
    };

    private string GetFeatsClass(int check) => check switch
    {
        1 => "correct",
        2 => "partial",
        _ => "incorrect"
    };

    private string GetIntArrow(int check) => check switch
    {
        0 => " ⬆️",
        2 => " ⬇️",
        _ => ""
    };

    private List<string> SplitTitleIntoWords(string title)
    {
        return title.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(word => word.Trim())
                    .ToList();
    }
    
    private string FormatDuration(int durationMs)
{
    var ts = TimeSpan.FromMilliseconds(durationMs);
    return $"{(int)ts.TotalMinutes}:{ts.Seconds:D2}";
}
    private void NavigateToAlbumComponent()
    {
        navigationManager.NavigateTo("/album");
    }
}
