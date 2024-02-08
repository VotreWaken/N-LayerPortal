﻿using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.Interfaces;
using MusicPortal.BLL.ModelsDTO;
using MusicPortal.DAL.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace MusicPortal.BLL.Services
{
    public class AccountService : IAccountService
    {
        IUnitOfWorks Database { get; set; }

        public AccountService(IUnitOfWorks unit)
        {
            Database = unit;
        }
        public async Task<int> Create(UserDTO userDTO)
        {
            var user = new User()
            {
                Id = userDTO.Id,
                Login = userDTO.Login,
                Password = userDTO.Password,
				Salt = userDTO.Salt,
				ImageId = userDTO.ImageId,
				IsAdmin = userDTO.IsAdmin,
				IsAuth = userDTO.IsAuth,
			};
            var createdUser = await Database.Users.Create(user);
            return createdUser.Id;
        }

        public async Task Delete(int id)
        {
            await Database.Users.Delete(id);
        }

        public async Task<List<UserDTO>> GetAll()
        {
            List<UserDTO> users = new List<UserDTO>();

            foreach (var item in await Database.Users.GetAll())
            {
                users.Add(new UserDTO { Id = item.Id, Login = item.Login });
            }

            return users.ToList();
        }

        public async Task<UserDTO> GetById(int id)
        {
            User userEntity = await Database.Users.GetById(id);
            UserDTO userDTO = new UserDTO
            {
                Id = userEntity.Id,
                Login = userEntity.Login
            };
            return userDTO;
        }

        public async Task<UserDTO> GetByLogin(string login)
        {
            User userEntity = await Database.Users.GetByLogin(login);

            if (userEntity != null)
            {
                UserDTO userDTO = new UserDTO
                {
                    Id = userEntity.Id,
                    Login = userEntity.Login,
                    Password = userEntity.Password,
                    ImageId = userEntity.ImageId,
                    IsAdmin = userEntity.IsAdmin,
                    IsAuth = userEntity.IsAuth,
                    Salt = userEntity.Salt,
                };

				return userDTO;
			}

            return null;
        }

        public async Task Update(UserDTO userDTO)
        {
            User user = new User
            {
                Id = userDTO.Id,
                Login = userDTO.Login
            };
            await Database.Users.Update(user);
        }
    }
}