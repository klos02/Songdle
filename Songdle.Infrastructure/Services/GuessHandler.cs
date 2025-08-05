using System;
using Songdle.Application.DTOs;
using Songdle.Application.Interfaces;

namespace Songdle.Infrastructure.Services;

public class GuessHandler : IGuessHandler
{
    public Task<AnswerCheckDto> GuessSong(AnswerDto answer, TodaysGameDto todaysGame)
    {
        if (answer == null || todaysGame == null || todaysGame.SongOfTheDay == null)
        {
            throw new ArgumentNullException("Answer or Today's Game cannot be null.");
        }

        bool titleCheck = string.Equals(answer.Title, todaysGame.SongOfTheDay.Title, StringComparison.OrdinalIgnoreCase);
        bool containsAny = false;

        if (!titleCheck)
        {
            var titleWords = answer.Title.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            containsAny = titleWords.Any(word => todaysGame.SongOfTheDay.Title.Contains(word, StringComparison.OrdinalIgnoreCase));
        }

        bool featsCheck = answer.Feats.OrderBy(x => x).SequenceEqual(todaysGame.SongOfTheDay.Feats?.OrderBy(x => x) ?? Enumerable.Empty<string>());
        bool anyMatchingFeats = false;

        if (!featsCheck)
        {
            anyMatchingFeats = answer.Feats.Any(feat => todaysGame.SongOfTheDay.Feats?.Contains(feat, StringComparer.OrdinalIgnoreCase) == true);
        }


        var answerCheck = new AnswerCheckDto
        {
            TitleCheck = titleCheck ? 1 : containsAny ? 2 : 0,
            ArtistCheck = string.Equals(answer.Artist, todaysGame.SongOfTheDay?.Artist, StringComparison.OrdinalIgnoreCase),
            FeatsCheck = featsCheck ? 1 : anyMatchingFeats ? 2 : 0,
            AlbumCheck = string.Equals(answer.Album, todaysGame.SongOfTheDay?.Album, StringComparison.OrdinalIgnoreCase),
            ReleaseDateCheck = answer.ReleaseDate.Date == todaysGame.SongOfTheDay?.ReleaseDate.Date ? 1
                : answer.ReleaseDate.Date < todaysGame.SongOfTheDay?.ReleaseDate.Date ? 0 : 2,
            GenreCheck = string.Equals(answer.Genre, todaysGame.SongOfTheDay?.Genre, StringComparison.OrdinalIgnoreCase),
            CountryCheck = string.Equals(answer.Country, todaysGame.SongOfTheDay?.Country, StringComparison.OrdinalIgnoreCase)
        };

        return Task.FromResult(answerCheck);
    }
}
