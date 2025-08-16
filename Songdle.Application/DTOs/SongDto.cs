using System;

namespace Songdle.Application.DTOs;

public class SongDto
{
    public int Id { get; set; }
    public string SpotifyId { get; set; } = string.Empty;
    public string Title { get; set; }
    public string Artist { get; set; }
    public List<string> Feats { get; set; } = [];
    public string Album { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Genre { get; set; }
    public string? LyricsFragment { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string? ImageUrl { get; set; }
    public string? AudioPreviewUrl { get; set; }
    public int Popularity { get; set; } = 0;
}
