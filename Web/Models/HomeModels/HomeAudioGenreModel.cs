using MusicPortal.BLL.ModelsDTO;
using MusicPortal.Models.GenreModels;
using MusicPortal.Models.SongsModels;

namespace MusicPortal.Models.HomeModels
{
    public class HomeAudioGenreModel
    {
        public GenreDTO Genre { get; set; }
        public IEnumerable<AudioDTO> Songs { get; set; }
        public IEnumerable<string> ImagePaths { get; set; }
    }
}
