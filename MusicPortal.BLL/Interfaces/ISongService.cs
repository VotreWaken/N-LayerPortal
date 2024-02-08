using MusicPortal.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Interfaces
{
    public interface ISongService
    {
        Task<List<AudioDTO>> GetAll();
        Task<AudioDTO> GetById(int id);
        Task<AudioDTO> Create(AudioDTO image);
        Task Update(AudioDTO image);
        Task Delete(int id);

        Task<List<AudioDTO>> GetSongsByGenreAsync(string genreName);
    }
}
