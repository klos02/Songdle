using System;

namespace Songdle.Application.DTOs;

public class AnswerDto
{
    public int Id { get; set; }
    public string? SpotifyId { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string Artist { get; set; } = string.Empty;
    public List<string> Feats { get; set; } = [];
    public string? Album { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int? Duration { get; set; }
    
}
