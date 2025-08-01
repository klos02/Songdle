using System;

namespace Songdle.Application.DTOs;

public class TodaysGameDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public SongDto? SongOfTheDay { get; set; }
    public int SongOfTheDayId { get; set; }
}
