using System;
using Songdle.Application.DTOs;
using Songdle.Application.Interfaces;

namespace Songdle.Infrastructure.Services;

public class AlbumHandler : IAlbumHandler
{
    public Task<AlbumDto> GetAlbumByIdAsync(string albumId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<AlbumDto>> SearchAlbumsByNameAsync(string albumName)
    {
        throw new NotImplementedException();
    }
}
