using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Context;
using MusicPortal.BLL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


var configuration = builder.Configuration;


builder.Services.AddControllersWithViews();


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();


string? connection = configuration.GetConnectionString("DefaultConnection");


builder.Services.AddMusicPortalContext(connection);


builder.Services.AddUnitOfWorkService();
builder.Services.AddBLLServices();

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
