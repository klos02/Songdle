using System;

namespace Songdle.Application.DTOs;

public class AnswerCheckDto
{
    public int TitleCheck { get; set; }
    public bool ArtistCheck { get; set; }
    public int FeatsCheck { get; set; }
    public bool AlbumCheck { get; set; }
    public int ReleaseDateCheck { get; set; }
    public bool GenreCheck { get; set; }
    public bool CountryCheck { get; set; }
}
