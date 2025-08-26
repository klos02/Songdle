using System;

namespace Songdle.Application.DTOs;

public class AlbumDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string ImageUrl { get; set; }
    public string AlbumType { get; set; }
}
