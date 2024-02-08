using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> GetByLogin(string login);
        Task<User> Create(User user);
        Task Update(User user);
        Task Delete(int id);
    }
}
