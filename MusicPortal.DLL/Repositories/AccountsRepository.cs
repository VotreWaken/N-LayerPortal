using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Context;
using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Repositories
{
    public class AccountsRepository : IAccountRepository, IRepository<User>
    {

        // Context 
        private UserContext _context;

        // Constructor
        public AccountsRepository(UserContext context) => _context = context;

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

            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
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
