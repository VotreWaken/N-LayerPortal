using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Context;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Repositories
{
    public class EFUnitOfWorks : IUnitOfWorks
    {
        private UserContext db;
        private GenreRepository genreRepository;
        private SongRepository songRepository;
        private AccountsRepository userRepository;
        private ImageRepository imageRepository;
        private AudioGenreRepository audioGenreRepository;
        public EFUnitOfWorks(UserContext db)
        {
            this.db = db;
        }
        public IAccountRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new AccountsRepository(db);
                return userRepository;
            }
        }
        public IAudioGenre AudioGenre
        {
            get
            {
                if (audioGenreRepository == null)
                    audioGenreRepository = new AudioGenreRepository(db);
                return audioGenreRepository;
            }
        }
        public IRepository<Genre> Genres
        {
            get
            {
                if (genreRepository == null)
                    genreRepository = new GenreRepository(db);
                return genreRepository;
            }
        }

        public IAudioRepository Audio
        {
            get
            {
                if (songRepository == null)
                    songRepository = new SongRepository(db);
                return songRepository;
            }
        }

        public IRepository<Image> Image
        {
            get
            {
                if (imageRepository == null)
                    imageRepository = new ImageRepository(db);
                return imageRepository;
            }
        }
        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }
}
