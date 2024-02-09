using MusicPortal.DAL.Context;
using MusicPortal.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Repositories
{
    public class GenreRepository : IRepository<Genre>
    {
        // Context 
        private readonly UserContext _context;

        // Constructor 
        public GenreRepository(UserContext context) => _context = context;

        // Get All Genres
        public async Task<List<Genre>> GetAll()
        {
            return await _context.Genre.ToListAsync();
        }

        // Get By Id Genre
        public async Task<Genre> GetById(int id)
        {
            return await _context.Genre.FindAsync(id);
        }
        // Create Genre
        public async Task<Genre> Create(Genre genre)
        {
            _context.Genre.Add(genre);
            await _context.SaveChangesAsync();
            return genre;
        }
        // Update Genre
        public async Task Update(Genre genre)
        {
            _context.Entry(genre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Delete Genre
        public async Task Delete(int id)
        {
            var genre = await _context.Genre.FindAsync(id);
            if (genre != null)
            {
                _context.Genre.Remove(genre);
                await _context.SaveChangesAsync();
            }
        }
    }
}
