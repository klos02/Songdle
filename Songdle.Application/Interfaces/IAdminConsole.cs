using System;

namespace Songdle.Application.Interfaces;

public interface IAdminConsole
{
    public Task AddSongFromJsonAsync(string json);
}
