using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Interfaces
{
    public interface IService<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Create(T image);
        Task Update(T image);
        Task Delete(int id);
    }
}
