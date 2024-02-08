using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.ModelsDTO
{
    public class UserDTO
    {
		public int Id { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public string Salt { get; set; }
		public int ImageId { get; set; }
		public bool IsAdmin { get; set; }
		public bool IsAuth { get; set; }
		public ImageDTO Image { get; set; }
	}
}
