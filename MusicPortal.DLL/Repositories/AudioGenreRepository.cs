using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Context;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Repositories
{
    public class AudioGenreRepository : IAudioGenre
    {
        private readonly UserContext _context;

        public AudioGenreRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<List<AudioGenre>> GetAll()
        {
            return await _context.AudioGenre.ToListAsync();
        }

        public async Task<AudioGenre> GetById(int audioId, int genreId)
        {
            return await _context.AudioGenre.FindAsync(audioId, genreId);
        }

        public async Task<AudioGenre> Create(AudioGenre audioGenre)
        {
            _context.AudioGenre.Add(audioGenre);
            await _context.SaveChangesAsync();
            return audioGenre;
        }

        public async Task Update(AudioGenre audioGenre)
        {
            _context.Entry(audioGenre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int audioId, int genreId)
        {
            var audioGenre = await _context.AudioGenre.FindAsync(audioId, genreId);
            if (audioGenre != null)
            {
                _context.AudioGenre.Remove(audioGenre);
                await _context.SaveChangesAsync();
            }
        }

        //public async Task<List<Audio>> GetSongsByGenre(string genreName)
        //{
        //    return await _context.Audio
        //        .Where(a => a.AudioGenres.Any(ag => ag.Genre.Name == genreName))
        //        .ToListAsync();
        //}
    }
}
