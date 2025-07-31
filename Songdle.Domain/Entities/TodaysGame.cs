using System;

namespace Songdle.Domain.Entities;

public class TodaysGame
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public Song? SongOfTheDay { get; set; }
}
