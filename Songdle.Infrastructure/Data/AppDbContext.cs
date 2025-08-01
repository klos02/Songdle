using System;
using Microsoft.EntityFrameworkCore;
using Songdle.Domain.Entities;

namespace Songdle.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Song> Songs { get; set; }
    public DbSet<TodaysGame> Games { get; set; }


    
}
