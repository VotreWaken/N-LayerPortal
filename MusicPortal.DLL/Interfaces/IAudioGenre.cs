using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Interfaces
{
    public interface IAudioGenre
    {
        // Get All Audio with Particular Genre
        Task<List<AudioGenre>> GetAll();

        // Create Audio with Particular Genre
        Task<AudioGenre> Create(AudioGenre audio);

        // Update Audio with Particular Genre
        Task Update(AudioGenre audioGenreDTO);

        // Get by Id Audio with Particular Genre
        Task<AudioGenre> GetById(int audioId, int genreId);

        // Delete Audio with Particular Genre
        Task Delete(int audioId, int genreId);
    }
}
