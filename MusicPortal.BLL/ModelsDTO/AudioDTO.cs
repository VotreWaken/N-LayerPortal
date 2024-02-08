using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.ModelsDTO
{
    public class AudioDTO
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int ImageId { get; set; }
    }
}
