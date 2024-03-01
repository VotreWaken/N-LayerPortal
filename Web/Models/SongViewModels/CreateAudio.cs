using MusicPortal.Models.AccountModels;

namespace MusicPortal.Models.SongsModels
{
	public class CreateAudio
	{
		public int Id { get; set; }
		public string Path { get; set; }
		public string Name { get; set; }
		public int UserId { get; set; }
		public int ImageId { get; set; }
		public List<int> SelectedGenres { get; set; }
	}
}
