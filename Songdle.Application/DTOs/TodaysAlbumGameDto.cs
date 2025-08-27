using System;

namespace Songdle.Application.DTOs;

public class TodaysAlbumGameDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string? SpotifyAlbumId { get; set; } = null!;
    public string? AlbumName { get; set; } = null!;
    public string? AlbumImageUrl { get; set; } = null!;
}
