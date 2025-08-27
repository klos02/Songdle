using System;
using Songdle.Application.DTOs;
using Songdle.Application.Interfaces;

namespace Songdle.Application.Services;

public class AlbumProcessingService(IAlbumHandler albumHandler) : IAlbumProcessingService
{
    public async Task<AlbumDto> GetAlbumByIdAsync(string albumId)
    {
        return await albumHandler.GetAlbumByIdAsync(albumId);
    }

    public async Task<IEnumerable<AlbumDto>> SearchAlbumsByNameAsync(string albumName)
    {
        return await albumHandler.SearchAlbumsByNameAsync(albumName);
    }
}
