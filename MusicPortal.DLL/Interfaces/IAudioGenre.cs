using MusicPortal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Interfaces
{
    public interface IAudioGenre
    {
        Task<List<AudioGenre>> GetAll();
        Task<AudioGenre> Create(AudioGenre audio);
        Task Update(AudioGenre audioGenreDTO);

        // Task<List<Audio>> GetSongsByGenre(string genreName);
        Task<AudioGenre> GetById(int audioId, int genreId);
        Task Delete(int audioId, int genreId);
    }
}
