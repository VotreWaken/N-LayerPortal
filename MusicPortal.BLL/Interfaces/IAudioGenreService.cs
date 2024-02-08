using MusicPortal.BLL.ModelsDTO;
using MusicPortal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Interfaces
{
    public interface IAudioGenreService
    {
        Task<AudioGenreDTO> GetById(int audioId, int genreId);
        Task<List<AudioGenreDTO>> GetAll();
        Task<int> Create(AudioGenreDTO audioGenreDTO);
        Task Update(AudioGenreDTO audioGenreDTO);
        Task Delete(int audioId, int genreId);
    }
}
