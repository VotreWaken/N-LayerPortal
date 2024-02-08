using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPortal.DAL.Entities;
namespace MusicPortal.DAL.Context
{
    public class UserContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Audio> Audio { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<AudioGenre> AudioGenre { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Конфигурация Many-to-Many связи
            modelBuilder.Entity<AudioGenre>()
                .HasKey(ag => new { ag.AudioId, ag.GenreId });

            modelBuilder.Entity<AudioGenre>()
                .HasOne(ag => ag.Audio)
                .WithMany(a => a.AudioGenres)
                .HasForeignKey(ag => ag.AudioId);

            modelBuilder.Entity<AudioGenre>()
                .HasOne(ag => ag.Genre)
                .WithMany(g => g.AudioGenres)
                .HasForeignKey(ag => ag.GenreId);
        }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
            //if (Database.EnsureCreated())
            //{
            //    //User user = new User()
            //    //{
            //    //    Login = "admin",
            //    //    Password = "admin",
            //    //    IsAdmin = true
            //    //};
            //    // Sha256 sha256 = new Sha256();
            //    // sha256.ComputeSalt();
            //    // user.Password = sha256.ComputeHash(sha256.ComputeSalt(), user.Password);

            //    //Genre newGenre = new Genre
            //    //{
            //    //    GenreName = "Pop"
            //    //};

            //    // Добавляем жанр в DbSet и сохраняем изменения в базе данных
            //    // _context.Genres.Add(newGenre);
            //    // await _context.SaveChangesAsync();
            //}
        }
    }
}
