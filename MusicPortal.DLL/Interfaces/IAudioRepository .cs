using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Interfaces
{
	public interface IAudioRepository : IRepository<Audio>
	{
        // Get All Audio with Particular Genre
        Task<List<Audio>> GetSongsByGenre(string genreName);
	}
}
