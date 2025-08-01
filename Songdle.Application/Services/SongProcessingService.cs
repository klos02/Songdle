using System;
using Songdle.Application.DTOs;
using Songdle.Application.Interfaces;


namespace Songdle.Application.Services;

public class SongProcessingService(ISongHandler songHandler) : ISongProcessingService
{
    public async Task<IEnumerable<SongDto>> GetAllSongsAsync()
    {
        var songs = await songHandler.GetAllSongsAsync();
        return songs.Select(song => new SongDto
        {
            Id = song.Id,
            Title = song.Title,
            Artist = song.Artist,
            Album = song.Album,
            Genre = song.Genre,
            ReleaseDate = song.ReleaseDate,
            LyricsFragment = song.LyricsFragment,
            Country = song.Country,
            ImageUrl = song.ImageUrl,
            AudioPreviewUrl = song.AudioPreviewUrl,
            Feats = song.Feats ?? [] 
            
        });
        
    }

    public async Task<SongDto?> GetSongByIdAsync(int id)
    {
        try
        {
            var song = await songHandler.GetSongByIdAsync(id);
            if (song == null)
            {
                return null;
            }
            return new SongDto
            {
                Id = song.Id,
                Title = song.Title,
                Artist = song.Artist,
                Album = song.Album,
                Genre = song.Genre,
                ReleaseDate = song.ReleaseDate,
                LyricsFragment = song.LyricsFragment,
                Country = song.Country,
                ImageUrl = song.ImageUrl,
                AudioPreviewUrl = song.AudioPreviewUrl,
                Feats = song.Feats ?? []
            };
        }
        catch (KeyNotFoundException)
        {
            return null;
        }
            
    }

    
}
