using MusicPortal.DAL.Context;
using MusicPortal.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Entities;
namespace MusicPortal.DAL.Repositories
{
    public class SongRepository : IAudioRepository
    {
        // Context 
        private readonly UserContext _context;

        // Constructor
        public SongRepository(UserContext context)
        {
            this._context = context;
        }
        // Get All Audio 
        public async Task<List<Audio>> GetAll()
        {
            return await _context.Audio.ToListAsync();
        }
        // Get Audio By Id
        public async Task<Audio> GetById(int id)
        {
            return await _context.Audio.FindAsync(id);
        }
        // Create Audio 
        public async Task<Audio> Create(Audio audio)
        {
            _context.Audio.Add(audio);
            await _context.SaveChangesAsync();
            return audio;
        }
        // Update Audio 
        public async Task Update(Audio audio)
        {
            _context.Entry(audio).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        // Delete Audio 
        public async Task Delete(int id)
        {
            var audio = await _context.Audio.FindAsync(id);
            if (audio != null)
            {
                _context.Audio.Remove(audio);
                await _context.SaveChangesAsync();
            }
        }
        // Get Songs By Genre
        public async Task<List<Audio>> GetSongsByGenre(string genreName)
        {
            return await _context.Audio
                .Where(a => a.AudioGenres.Any(ag => ag.Genre.Name == genreName))
                .ToListAsync();
        }

        // Get Songs By User

        public async Task<List<Audio>> GetSongsByUser(string userName)
        {
            return await _context.Audio
                .Where(a => a.Author == userName)
                .ToListAsync();
        }
    }
}
