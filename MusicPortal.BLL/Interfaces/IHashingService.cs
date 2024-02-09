using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Interfaces
{
    public interface IHashingService
    {
        // Compute Hash Method
        Task<string> ComputeHash(string salt, string data);

        // Compute Salt Method
        Task<string> ComputeSalt();

    }
}
