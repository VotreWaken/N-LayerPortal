using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Context;
using MusicPortal.BLL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Получаем конфигурацию
var configuration = builder.Configuration;

// Добавляем сервисы для работы с MVC
builder.Services.AddControllersWithViews();

// Добавляем распределенный кэш и сессии
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// Получаем строку подключения к базе данных из конфигурации
string? connection = configuration.GetConnectionString("DefaultConnection");

// Регистрируем контекст базы данных
builder.Services.AddMusicPortalContext(connection);

// Регистрируем другие сервисы
builder.Services.AddUnitOfWorkService();
builder.Services.AddBLLServices();

var app = builder.Build();

// Настраиваем сессии и HTTPS-перенаправление
app.UseSession();
app.UseHttpsRedirection();

// Разрешаем использование статических файлов
app.UseStaticFiles();

// Настраиваем маршрутизацию
app.UseRouting();

// Настраиваем авторизацию
app.UseAuthorization();

// Настраиваем обработку запросов к контроллерам
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Запускаем приложение
app.Run();
