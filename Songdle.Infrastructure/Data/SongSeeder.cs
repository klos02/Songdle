using Microsoft.EntityFrameworkCore;
using Songdle.Domain.Entities; 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Songdle.Infrastructure.Data;
public static class SongSeeder
{
    public static async Task SeedAsync(DbContext context)
    {
        if (context.Set<Song>().Any())
            return;

        var songs = new List<Song>
        {
            new() {
                Title = "Pop Out",
                Artist = "Playboi Carti",
                Feats = new List<string>(),
                Album = "I Am Music",
                ReleaseDate = new DateTime(2025, 3, 14),
                Genre = "Hip-Hop/Rap",
                LyricsFragment = "Yeah, I pop out at night...",
                Country = "USA"
            },
            new() {
                Title = "Crush",
                Artist = "Playboi Carti",
                Feats = new List<string> { "Travis Scott" },
                Album = "I Am Music",
                ReleaseDate = new DateTime(2025, 3, 14),
                Genre = "Hip-Hop/Rap",
                LyricsFragment = "Feelin' like a crush, yeah",
                Country = "USA"
            },

        };

        await context.Set<Song>().AddRangeAsync(songs);
        await context.SaveChangesAsync();
    }
}
