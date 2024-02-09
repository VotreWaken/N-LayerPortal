using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Entities
{
    public class Audio
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int ImageId { get; set; }
        public ICollection<AudioGenre>? AudioGenres { get; set; }
    }
}
