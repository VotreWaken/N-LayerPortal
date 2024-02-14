using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Context;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Repositories
{
    public class EFUnitOfWorks : IUnitOfWorks
    {
        // Context
        private UserContext db;
        
        // Repositories 
        private GenreRepository? genreRepository;
        private SongRepository? songRepository;
        private AccountsRepository? userRepository;
        private ImageRepository? imageRepository;
        private AudioGenreRepository? audioGenreRepository;

        // Constructor
        public EFUnitOfWorks(UserContext db)
        {
            this.db = db;
        }

        // User
        public IAccountRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new AccountsRepository(db);
                return userRepository;
            }
        }

        // AudioGenre
        public IAudioGenre AudioGenre
        {
            get
            {
                if (audioGenreRepository == null)
                    audioGenreRepository = new AudioGenreRepository(db);
                return audioGenreRepository;
            }
        }

        // Genre
        public IRepository<Genre> Genre
        {
            get
            {
                if (genreRepository == null)
                    genreRepository = new GenreRepository(db);
                return genreRepository;
            }
        }

        // Audio 
        public IAudioRepository Audio
        {
            get
            {
                if (songRepository == null)
                    songRepository = new SongRepository(db);
                return songRepository;
            }
        }

        // Image
        public IRepository<Image> Image
        {
            get
            {
                if (imageRepository == null)
                    imageRepository = new ImageRepository(db);
                return imageRepository;
            }
        }

        // Save
        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }
}
