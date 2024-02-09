using MusicPortal.Models.AccountModels;

namespace MusicPortal.Models.SongsModels
{
    public class AudioPath
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public int ImageId { get; set; }
        public User User { get; set; }
    }
}
