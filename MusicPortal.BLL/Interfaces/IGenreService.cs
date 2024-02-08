using MusicPortal.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Interfaces
{
    public interface IGenreService
    {
        Task<List<GenreDTO>> GetAll();
        Task<GenreDTO> GetById(int id);
        Task<GenreDTO> Create(GenreDTO image);
        Task Update(GenreDTO image);
        Task Delete(int id);
    }
}
