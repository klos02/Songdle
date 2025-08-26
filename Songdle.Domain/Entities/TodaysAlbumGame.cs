using System;

namespace Songdle.Domain.Entities;

public class TodaysAlbumGame
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string? SpotifyAlbumId { get; set; } = null!;
    public string? AlbumName { get; set; } = null!;
    public string? AlbumImageUrl { get; set; } = null!;
}
