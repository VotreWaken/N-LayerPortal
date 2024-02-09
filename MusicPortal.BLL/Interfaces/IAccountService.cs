using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPortal.BLL.ModelsDTO;
namespace MusicPortal.BLL.Interfaces
{
    public interface IAccountService // : IService<UserDTO>
    {
        Task<UserDTO> GetById(int id);
        Task<List<UserDTO>> GetAll();
        Task<UserDTO> GetByLogin(string login);
        Task<int> Create(UserDTO userDTO);
        Task Update(UserDTO userDTO);
        Task Delete(int id);
        Task<bool> ValidateUserPassword(UserDTO userDTO, string password);
    }
}
