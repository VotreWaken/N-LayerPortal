using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Context;
using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Entities.Hashing;

namespace MusicPortal.DAL.Repositories
{
    public class AccountsRepository : IAccountRepository
    {
        // Context 
        private UserContext _context;

        // Hashing Service
        private readonly IHash _hashService = new Sha256();

        // Constructor
        public AccountsRepository(UserContext context)
        {
            _context = context;
        }

        // Get All Users 
        public async Task<List<User>> GetAll()
        {
            return await _context.User.ToListAsync();
        }

        // Get User By Login 
        public async Task<User> GetByLogin(string login)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Login == login);
        }
        // Get User By Id
        public async Task<User> GetById(int id)
        {
            return await _context.User.FindAsync(id);
        }
        // Create User 
        public async Task<User> Create(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await HashUserPassword(user);

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        // Hash Password 
        public async Task HashUserPassword(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                throw new ArgumentNullException(nameof(user.Password));
            }

            string salt = await _hashService.ComputeSalt();
            string hashedPassword = await _hashService.ComputeHash(salt, user.Password);

            user.Password = hashedPassword;
            user.Salt = salt;
        }

        // Compare User Password with Salt 
        public async Task<bool> ValidatePassword(User user, string password)
        {
            string salt = user.Salt;

            string hashedPassword = await _hashService.ComputeHash(salt, password);

            return user.Password == hashedPassword;
        }

        // Update User
        public async Task Update(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.User.Update(user);
            await _context.SaveChangesAsync();
        }
        // Delete User
        public async Task Delete(int id)
        {
            var user = await GetById(id);
            if (user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
