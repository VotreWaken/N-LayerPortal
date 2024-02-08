using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.Interfaces;
using MusicPortal.BLL.ModelsDTO;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Repositories;

namespace MusicPortal.BLL.Services
{
    public class GenreService : IGenreService
    {
        IUnitOfWorks Database { get; set; }

        public GenreService(IUnitOfWorks unit)
        {
            Database = unit;
        }
        public async Task<List<GenreDTO>> GetAll()
        {
            var genres = await Database.Genres.GetAll();
            return genres.Select(genre => new GenreDTO
            {
                Id = genre.Id,
                Name = genre.Name
            }).ToList();
        }
        public async Task<GenreDTO> GetById(int id)
        {
            var genreEntity = await Database.Genres.GetById(id);
            if (genreEntity == null)
            {
                throw new Exception($"Genre with id {id} not found.");
            }

            return new GenreDTO
            {
                Id = genreEntity.Id,
                Name = genreEntity.Name
            };
        }

        public async Task<GenreDTO> Create(GenreDTO genreDto)
        {
            var genreEntity = new Genre
            {
                Name = genreDto.Name
            };

            genreEntity = await Database.Genres.Create(genreEntity);

            return new GenreDTO
            {
                Id = genreEntity.Id,
                Name = genreEntity.Name
            };
        }

        public async Task Update(GenreDTO genreDto)
        {
            var genreEntity = await Database.Genres.GetById(genreDto.Id);
            if (genreEntity == null)
            {
                throw new Exception($"Genre with id {genreDto.Id} not found.");
            }

            genreEntity.Name = genreDto.Name;

            await Database.Genres.Update(genreEntity);
        }

        public async Task Delete(int id)
        {
            var genreEntity = await Database.Genres.GetById(id);
            if (genreEntity == null)
            {
                throw new Exception($"Genre with id {id} not found.");
            }

            await Database.Genres.Delete(id);
        }
    }
}
