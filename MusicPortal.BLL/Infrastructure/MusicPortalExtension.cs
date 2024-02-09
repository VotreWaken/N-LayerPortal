using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Context;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Services;

namespace MusicPortal.BLL.Infrastructure
{
    public static class MusicPortalExtension
    {
        public static void AddMusicPortalContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<UserContext>(options => options.UseSqlServer(connection));
        }

        public static void AddBLLServices(this IServiceCollection services)
        {
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IGenreService, GenreService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<ISongService, SongService>();
            services.AddTransient<IAudioGenreService, AudioGenreService>();

        }
    }
}
