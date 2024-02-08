using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Create(T image);
        Task Update(T image);
        Task Delete(int id);
    }
}
