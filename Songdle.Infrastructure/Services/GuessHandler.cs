using System;
using Songdle.Application.DTOs;
using Songdle.Application.Interfaces;

namespace Songdle.Infrastructure.Services;

public class GuessHandler(ISongHandler songHandler) : IGuessHandler
{
    public async Task<AnswerCheckDto> GuessSong(AnswerDto answer, TodaysGameDto todaysGame)
    {

        var songOfTheDay = await songHandler.GetSongByIdAsync(todaysGame.SpotifySongId);

        if (answer == null || todaysGame == null || todaysGame.SpotifySongId == null)
        {
            throw new ArgumentNullException("Answer or Today's Game cannot be null.");
        }

        Console.WriteLine($"Sprawdzanie odpowiedzi: {answer.Title} - {answer.Artist} (ID: {todaysGame.SpotifySongId})");
        bool titleCheck = string.Equals(answer.Title, songOfTheDay.Title, StringComparison.OrdinalIgnoreCase);
        bool containsAny = false;

        if (!titleCheck)
        {
            var titleWords = answer.Title.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            containsAny = titleWords.Any(word => songOfTheDay.Title.Contains(word, StringComparison.OrdinalIgnoreCase));
        }

        bool featsCheck = answer.Feats.OrderBy(x => x).SequenceEqual(songOfTheDay.Feats?.OrderBy(x => x) ?? Enumerable.Empty<string>());
        bool anyMatchingFeats = false;

        if (!featsCheck)
        {
            anyMatchingFeats = answer.Feats.Any(feat => songOfTheDay.Feats?.Contains(feat, StringComparer.OrdinalIgnoreCase) == true);
        }


        var answerCheck = new AnswerCheckDto
        {
            TitleCheck = titleCheck ? 1 : containsAny ? 2 : 0,
            ArtistCheck = string.Equals(answer.Artist, songOfTheDay?.Artist, StringComparison.OrdinalIgnoreCase),
            FeatsCheck = featsCheck ? 1 : anyMatchingFeats ? 2 : 0,
            AlbumCheck = string.Equals(answer.Album, songOfTheDay?.Album, StringComparison.OrdinalIgnoreCase),
            ReleaseDateCheck = answer.ReleaseDate.Date == songOfTheDay?.ReleaseDate.Date ? 1
                : answer.ReleaseDate.Date < songOfTheDay?.ReleaseDate.Date ? 0 : 2,
            //ADD Duration check
        };

        return answerCheck;
    }
}
