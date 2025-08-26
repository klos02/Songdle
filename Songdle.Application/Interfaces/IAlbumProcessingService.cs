using System;
using Songdle.Application.DTOs;

namespace Songdle.Application.Interfaces;

public interface IAlbumProcessingService
{
    Task<AlbumDto> GetAlbumByIdAsync(string albumId);
    Task<IEnumerable<AlbumDto>> SearchAlbumsByNameAsync(string albumName);
}
