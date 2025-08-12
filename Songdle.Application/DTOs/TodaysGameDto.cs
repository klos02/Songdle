using System;

namespace Songdle.Application.DTOs;

public class TodaysGameDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string? SpotifySongId { get; set; } = null!;
}
