using MusicPortal.BLL.ModelsDTO;

namespace MusicPortal.Models.AccountModels
{
    public class UserIndexViewModel
    {
        public UserDTO User { get; set; }
        public List<AudioDTO> audio { get; set; }
    }
}
