using Microsoft.EntityFrameworkCore;
using Songdle.Infrastructure.Data;

using Songdle.Presentation.Components;
using DotNetEnv;
using Songdle.Domain.Interfaces;
using Songdle.Infrastructure.Repositories;
using Songdle.Application.Interfaces;
using Songdle.Infrastructure.Services;
using Songdle.Application.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components;
using Songdle.Infrastructure.Spotify;
using Songdle.Infrastructure.Spotify.Options;

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

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/non-authorized";
    options.AccessDeniedPath = "/access-denied";
    options.Cookie.Name = "SongdleAuthCookie";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
    options.Cookie.SameSite = SameSiteMode.Lax;

     options.Events.OnRedirectToLogin = context =>
    {
        var redirectUri = context.RedirectUri;
        if (redirectUri.StartsWith("http://"))
        {
            redirectUri = redirectUri.Replace("http://", "https://");
        }
        context.Response.Redirect(redirectUri);
        return Task.CompletedTask;
    };
});

builder.Services.Configure<SpotifyOptions>(options =>
{
    options.ClientId = Environment.GetEnvironmentVariable("SPOTIFY_CLIENT_ID");
    options.ClientSecret = Environment.GetEnvironmentVariable("SPOTIFY_CLIENT_SECRET");
});


//builder.Services.AddScoped<ISongRepository, SongRepository>();
builder.Services.AddScoped<SpotifyAuthService>();
builder.Services.AddHttpClient<ISongRepository, SpotifySongRepository>();
// SPOTIFY
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITodaysGameRepository, TodaysGameRepository>();
builder.Services.AddScoped<ISongHandler, SongHandler>();
builder.Services.AddScoped<ISongProcessingService, SongProcessingService>();
builder.Services.AddScoped<ITodaysGameProcessingService, TodaysGameProcessingService>();
builder.Services.AddScoped<ITodaysGameHandler, TodaysGameHandler>();
builder.Services.AddScoped<IGuessHandler, GuessHandler>();
builder.Services.AddScoped<IGuessProcessingService, GuessProcessingService>();
builder.Services.AddScoped<IAdminConsole, AdminConsole>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped(sp =>
{
    var navigationManager = sp.GetRequiredService<NavigationManager>();
    return new HttpClient
    {
        BaseAddress = new Uri(navigationManager.BaseUri)
    };
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// // 🔹 SEED użytkownika admina
// using (var scope = app.Services.CreateScope())
// {
//     Env.Load();
//     var services = scope.ServiceProvider;
//     var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
//     var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

//     string adminEmail = Environment.GetEnvironmentVariable("ADMIN_USERNAME");
//     string adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");

    
//     if (!await roleManager.RoleExistsAsync("Admin"))
//     {
//         await roleManager.CreateAsync(new IdentityRole("Admin"));
//     }

    
//     var adminUser = await userManager.FindByEmailAsync(adminEmail);
//     if (adminUser == null)
//     {
//         adminUser = new IdentityUser
//         {
//             UserName = adminEmail,
//             Email = adminEmail,
//             EmailConfirmed = true
//         };
//         await userManager.CreateAsync(adminUser, adminPassword);
//         await userManager.AddToRoleAsync(adminUser, "Admin");
//     }
// }




app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorPages();
app.MapControllers();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();





app.Run();
