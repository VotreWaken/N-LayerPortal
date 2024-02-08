using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Context;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Interfaces;

namespace MusicPortal.DAL.Repositories
{
    public class ImageRepository : IRepository<Image>
    {

        // Context
        private readonly UserContext _context;

        // Constructor
        public ImageRepository(UserContext context)
        {
            _context = context;
        }
        // Get All Images
        public async Task<List<Image>> GetAll()
        {
            return await _context.Image.ToListAsync();
        }
        // Get Image By Id
        public async Task<Image> GetById(int id)
        {
            return await _context.Image.FindAsync(id);
        }

        // Create Image
        public async Task<Image> Create(Image image)
        {
            _context.Image.Add(image);
            await _context.SaveChangesAsync();
            return image;
        }

        // Update Image
        public async Task Update(Image image)
        {
            _context.Entry(image).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Delete Image
        public async Task Delete(int id)
        {
            var image = await _context.Image.FindAsync(id);
            if (image != null)
            {
                _context.Image.Remove(image);
                await _context.SaveChangesAsync();
            }
        }
    }
}
