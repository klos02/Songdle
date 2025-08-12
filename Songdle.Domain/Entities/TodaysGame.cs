using System;

namespace Songdle.Domain.Entities;

public class TodaysGame
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string? SpotifySongId { get; set; } = null!;
}
