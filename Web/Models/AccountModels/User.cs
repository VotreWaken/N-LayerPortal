using MusicPortal.Models.GenreModels;

namespace MusicPortal.Models.AccountModels
{
    public class User
    {
        public int Id { get; set; }
        public string? Login { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Salt { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsAuth { get; set; }
        public int ImageId { get; set; }
    }
}
