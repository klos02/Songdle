using System;

namespace Songdle.Application.DTOs;

public class AnswerCheckDto
{
    public int TitleCheck { get; set; }
    public List<int> TitleCheckIndexes { get; set; } = [];
    public bool ArtistCheck { get; set; }
    public int FeatsCheck { get; set; }
    public bool AlbumCheck { get; set; }
    public int ReleaseDateCheck { get; set; }
    public int DurationCheck { get; set; }
    public int PopularityCheck { get; set; }
}
