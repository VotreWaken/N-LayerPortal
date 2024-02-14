using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Context;
using Microsoft.Extensions.DependencyInjection;
using MusicPortal.BLL.Infrastructure;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using MusicPortal.IntegrationTests.PageAccessibilityTests;
namespace RazorPagesProject.Tests;

// <snippet1>
public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<UserContext>));

            services.Remove(dbContextDescriptor);

            var dbConnectionDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbConnection));

            services.Remove(dbConnectionDescriptor);

            // Create open SqliteConnection so EF won't automatically close it.
            services.AddSingleton<DbConnection>(container =>
            {
                var connection = new SqliteConnection("DataSource=:memory:");
                connection.Open();

                return connection;
            });

            services.AddDbContext<UserContext>((container, options) =>
            {
                var connection = container.GetRequiredService<DbConnection>();
                options.UseSqlite(connection);
            });



            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<UserContext>();
                db.Database.EnsureCreated(); // Ensure database is created
                                             // Здесь также может быть код для заполнения тестовыми данными
            }


			//// Применить миграции при старте теста
			//var context = new UserContext(options.Options);
			//context.Database.EnsureCreated();
			//context.Database.Migrate();

			// Аутентификация 
			// services.AddAuthentication("TestScheme")
			//    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("TestScheme", options => { });


			// services.AddUnitOfWorkService();
            // services.AddBLLServices();
        });

        builder.UseEnvironment("Development");
    }
}
// </snippet1>
