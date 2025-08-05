using Microsoft.EntityFrameworkCore;
using Songdle.Infrastructure.Data;

using Songdle.Presentation.Components;
using DotNetEnv;
using Songdle.Domain.Interfaces;
using Songdle.Infrastructure.Repositories;
using Songdle.Application.Interfaces;
using Songdle.Infrastructure.Services;
using Songdle.Application.Services;

Env.Load();


var builder = WebApplication.CreateBuilder(args);


string connectionString = $"server={Environment.GetEnvironmentVariable("DB_HOST")};" +
                          $"port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                          $"database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                          $"uid={Environment.GetEnvironmentVariable("DB_USER")};" +
                          $"password={Environment.GetEnvironmentVariable("DB_PASSWORD")};";

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 42))));

builder.Services.AddScoped<ISongRepository, SongRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITodaysGameRepository, TodaysGameRepository>();
builder.Services.AddScoped<ISongHandler, SongHandler>();
builder.Services.AddScoped<ISongProcessingService, SongProcessingService>();
builder.Services.AddScoped<ITodaysGameProcessingService, TodaysGameProcessingService>();
builder.Services.AddScoped<ITodaysGameHandler, TodaysGameHandler>();
builder.Services.AddScoped<IGuessHandler, GuessHandler>();
builder.Services.AddScoped<IGuessProcessingService, GuessProcessingService>();
builder.Services.AddScoped<IAdminConsole, AdminConsole>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();




app.Run();
