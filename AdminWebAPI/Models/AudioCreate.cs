namespace AdminWebAPI.Models
{
    public class AudioCreate
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public int ImageId { get; set; }
        public List<int> SelectedGenres { get; set; }
        public IFormFile SongPath { get; set; }
        public IFormFile ImagePath { get; set; }
    }
}
