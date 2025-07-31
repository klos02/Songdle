using System;

namespace Songdle.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    
}
