
using MusicPortal.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Interfaces
{
    public interface IImageService // : IService<ImageDTO>
    {
        Task<List<ImageDTO>> GetAll();
        Task<ImageDTO> GetById(int id);
        Task<ImageDTO> Create(ImageDTO image);
        Task Update(ImageDTO image);
        Task Delete(int id);
    }
}
