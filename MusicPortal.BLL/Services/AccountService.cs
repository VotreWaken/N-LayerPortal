using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.Interfaces;
using MusicPortal.BLL.ModelsDTO;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Repositories;

namespace MusicPortal.BLL.Services
{
    public class AccountService : IAccountService
    {
        IUnitOfWorks Database { get; set; }

        public AccountService(IUnitOfWorks unit)
        {
            Database = unit;
        }
        public async Task<UserDTO> Create(UserDTO userDTO)
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

            var createdUserDTO = new UserDTO
            {
                Id = createdUser.Id,
                Login = createdUser.Login,
                Password = createdUser.Password,
                Salt = createdUser.Salt,
                ImageId = createdUser.ImageId,
                IsAdmin = createdUser.IsAdmin,
                IsAuth = createdUser.IsAuth,
            };

            return createdUserDTO;
        }
        public async Task<bool> ValidateUserPassword(UserDTO userDTO, string password)
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
            return await Database.Users.ValidatePassword(user, password);
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
                users.Add(new UserDTO { Id = item.Id, Login = item.Login, ImageId = item.ImageId, IsAdmin = item.IsAdmin, IsAuth = item.IsAuth });
            }

            return users.ToList();
        }

        public async Task<UserDTO> GetById(int id)
        {
            User userEntity = await Database.Users.GetById(id);
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
            var existingUser = await Database.Users.GetById(userDTO.Id);
            if (existingUser == null)
            {
                throw new InvalidOperationException($"User with ID {userDTO.Id} not found.");
            }

            existingUser.Login = userDTO.Login;
            existingUser.Password = userDTO.Password;
            existingUser.Salt = userDTO.Salt;
            existingUser.ImageId = userDTO.ImageId;
            existingUser.IsAdmin = userDTO.IsAdmin;
            existingUser.IsAuth = userDTO.IsAuth;

            await Database.Users.Update(existingUser);
        }
    }
}
