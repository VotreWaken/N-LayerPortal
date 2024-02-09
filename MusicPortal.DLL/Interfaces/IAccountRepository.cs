using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Interfaces
{
    public interface IAccountRepository : IRepository<User>
    {
        // Get User By Login
        Task<User> GetByLogin(string login);

        // Hash User Password
        Task HashUserPassword(User user);

        // Validate User Password
        Task<bool> ValidatePassword(User user, string password);
    }
}
