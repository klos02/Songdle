using System;
using Songdle.Domain.Interfaces;
using Songdle.Infrastructure.Data;

namespace Songdle.Infrastructure.Repositories;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}
