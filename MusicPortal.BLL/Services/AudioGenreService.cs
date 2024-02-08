using AutoMapper;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.ModelsDTO;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.Services
{
    public class AudioGenreService : IAudioGenreService
    {
        IUnitOfWorks Database { get; set; }

        public AudioGenreService(IUnitOfWorks unit)
        {
            Database = unit;
        }

        public async Task<AudioGenreDTO> GetById(int audioId, int genreId)
        {
            var audioGenre = await Database.AudioGenre.GetById(audioId, genreId);
            return new AudioGenreDTO
            {
                AudioId = audioGenre.AudioId,
                GenreId = audioGenre.GenreId
            };
        }

        public async Task<List<AudioGenreDTO>> GetAll()
        {
            var audioGenres = await Database.AudioGenre.GetAll();
            var audioGenreDTOs = new List<AudioGenreDTO>();
            foreach (var audioGenre in audioGenres)
            {
                audioGenreDTOs.Add(new AudioGenreDTO
                {
                    AudioId = audioGenre.AudioId,
                    GenreId = audioGenre.GenreId
                });
            }
            return audioGenreDTOs;
        }

        public async Task<int> Create(AudioGenreDTO audioGenreDTO)
        {
            var audioGenre = new AudioGenre
            {
                AudioId = audioGenreDTO.AudioId,
                GenreId = audioGenreDTO.GenreId
            };
            var createdAudioGenre = await Database.AudioGenre.Create(audioGenre);
            return createdAudioGenre.AudioId;
        }

        public async Task Update(AudioGenreDTO audioGenreDTO)
        {
            var audioGenre = new AudioGenre
            {
                AudioId = audioGenreDTO.AudioId,
                GenreId = audioGenreDTO.GenreId
            };
            await Database.AudioGenre.Update(audioGenre);
        }

        public async Task Delete(int audioId, int genreId)
        {
            await Database.AudioGenre.Delete(audioId, genreId);
        }
    }
}
