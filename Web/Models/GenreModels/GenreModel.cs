using MusicPortal.BLL.ModelsDTO;
using System;

namespace MusicPortal.Models.GenreModels
{
    public class GenreModel
    {
        public ICollection<GenreDTO>? Genres { get; set; }

        public Genre NewGenre { get; set; }
    }
}
