using System;
using Songdle.Application.Interfaces;

namespace Songdle.Application.Services;

public class AdminConsole(ISongHandler songHandler) : IAdminConsole
{
    public async Task AddSongFromJsonAsync(string json)
    {
        await songHandler.AddSongFromJsonAsync(json);
    }
}
