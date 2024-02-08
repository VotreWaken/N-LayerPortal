using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Context;

namespace MusicPortal.BLL.Infrastructure
{
    public static class MusicPortalExtension
    {
        public static void AddMusicPortalContext(this IServiceCollection services, string connection)
        {
            services.AddDbContext<UserContext>(options => options.UseSqlServer(connection));
        }
    }
}
