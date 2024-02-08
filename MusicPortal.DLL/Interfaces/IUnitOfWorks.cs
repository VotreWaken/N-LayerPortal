using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Interfaces
{
    public interface IUnitOfWorks
    {
        public IAccountRepository Users { get; }
        public IRepository<Genre> Genres { get; }
        public IAudioRepository Audio { get; }
        public IRepository<Image> Image { get; }
        public IAudioGenre AudioGenre { get; }
        public Task Save();
    }
}
