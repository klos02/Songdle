using System;
using Songdle.Application.DTOs;
using Songdle.Domain.Entities;

namespace Songdle.Application.Interfaces;

public interface ISongProcessingService
{

    Task<SongDto?> GetSongByIdAsync(int id);
    Task<IEnumerable<SongDto>> GetAllSongsAsync();
    Task<SongDto?> GetSongByTitleAsync(string title);
    Task<IEnumerable<SongDto>> SearchSongsByTitleAsync(string partialTitle);
    
}
