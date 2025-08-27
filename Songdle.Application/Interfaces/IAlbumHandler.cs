using System;
using Songdle.Application.DTOs;

namespace Songdle.Application.Interfaces;

public interface IAlbumHandler
{
    Task<AlbumDto> GetAlbumByIdAsync(string albumId);
    Task<IEnumerable<AlbumDto>> SearchAlbumsByNameAsync(string albumName);
}
