namespace MusicPortal.Models.AccountModels
{
    public class ConfirmUsers
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public ICollection<User>? Users { get; set; }

        public Dictionary<int, string> ImagePaths { get; set; }
    }
}
