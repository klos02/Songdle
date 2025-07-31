using System;
using Songdle.Domain.Entities;

namespace Songdle.Application.Interfaces;

public interface ITodaysGame
{
    public Task<Song> GetSongOfTheDayAsync(DateTime date);
    public Task SetSongOfTheDayAsync(DateTime date, Song song);
    public Task<bool> IsSongOfTheDaySetAsync(DateTime date);
}
