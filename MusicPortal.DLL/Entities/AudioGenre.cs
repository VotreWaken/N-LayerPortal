using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Entities
{
    public class AudioGenre
    {
        public int AudioId { get; set; }
        public Audio Audio { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
