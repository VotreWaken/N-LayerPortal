using MusicPortal.Models.SongsModels;

namespace MusicPortal.Models.GenreModels
{
    public class AudioGenre
    {
        public int AudioId { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public AudioPath Audio { get; set; }
    }
}
