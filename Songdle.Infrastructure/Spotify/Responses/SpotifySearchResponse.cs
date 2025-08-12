using System;

namespace Songdle.Infrastructure.Spotify.Responses;

public class SpotifySearchResponse
{
    public Tracks tracks { get; set; }
}

public class Tracks
{
    public List<TrackItem> items { get; set; }
}

public class TrackItem
{
    public string id { get; set; }
    public string name { get; set; }
    public List<Artist> artists { get; set; }
    public Album album { get; set; }
    public string preview_url { get; set; }
}

public class Artist
{
    public string name { get; set; }
}

public class Album
{
    public string name { get; set; }
    public string release_date { get; set; }
    public List<Image> images { get; set; }
}

public class Image
{
    public string url { get; set; }
}
