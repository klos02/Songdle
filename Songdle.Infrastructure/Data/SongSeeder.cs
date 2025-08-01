using Microsoft.EntityFrameworkCore;
using Songdle.Domain.Entities; 


namespace Songdle.Infrastructure.Data;
public static class SongSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (context.Songs.Any())
            return;

        var songs = new List<Song>
        {
            new() {
                Title = "Pop Out",
                Artist = "Playboi Carti",
                Feats = [],
                Album = "I Am Music",
                ReleaseDate = new DateTime(2025, 3, 14),
                Genre = "Hip-Hop/Rap",
                LyricsFragment = "Yeah, I pop out at night...",
                Country = "USA"
            },
            new() {
                Title = "Crush",
                Artist = "Playboi Carti",
                Feats = ["Travis Scott"],
                Album = "I Am Music",
                ReleaseDate = new DateTime(2025, 3, 14),
                Genre = "Hip-Hop/Rap",
                LyricsFragment = "Feelin' like a crush, yeah",
                Country = "USA"
            },

        };

        await context.Songs.AddRangeAsync(songs);
        await context.SaveChangesAsync();
    }
}
