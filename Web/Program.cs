using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Services;
using MusicPortal.BLL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddMusicPortalContext(connection);
builder.Services.AddUnitOfWorkService();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<IImageService, ImageService>();
builder.Services.AddTransient<ISongService, SongService>();
builder.Services.AddTransient<IAudioGenreService, AudioGenreService>();

var app = builder.Build();



app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
