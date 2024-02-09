using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.ModelsDTO;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MusicPortal.BLL.Services
{
    public class SongService : ISongService
    {
        IUnitOfWorks Database { get; set; }

        public SongService(IUnitOfWorks unit)
        {
            Database = unit;
        }
        public async Task<List<AudioDTO>> GetAll()
        {
            var audios = await Database.Audio.GetAll();
            return audios.Select(audio => new AudioDTO
            {
                Id = audio.Id,
                Path = audio.Path,
                Title = audio.Title,
                Author = audio.Author,
                ImageId = audio.ImageId,
            }).ToList();
        }
        public async Task<AudioDTO> GetById(int id)
        {
            var audioEntity = await Database.Audio.GetById(id);

            if (audioEntity == null)
            {
                throw new Exception($"Audio with id {id} not found.");
            }

            var audioDto = new AudioDTO
            {
                Id = audioEntity.Id,
                Path = audioEntity.Path,
                Title = audioEntity.Title,
                Author = audioEntity.Author,
                ImageId = audioEntity.ImageId
            };

            return audioDto;
        }

        public async Task<AudioDTO> Create(AudioDTO audioDto)
        {
            var audioEntity = new Audio
            {
                Path = audioDto.Path,
                Title = audioDto.Title,
                Author = audioDto.Author,
                ImageId = audioDto.ImageId
            };

            var createdAudio = await Database.Audio.Create(audioEntity);
            await Database.Save();

            audioDto.Id = createdAudio.Id;
            return audioDto;
        }

        public async Task Update(AudioDTO audioDto)
        {
            var existingAudio = await Database.Audio.GetById(audioDto.Id);
            if (existingAudio == null)
            {
                throw new Exception($"Audio with id {audioDto.Id} not found.");
            }

            existingAudio.Path = audioDto.Path;
            existingAudio.Title = audioDto.Title;
            existingAudio.Author = audioDto.Author;
            existingAudio.ImageId = audioDto.ImageId;

            await Database.Audio.Update(existingAudio);
            await Database.Save();
        }

        public async Task Delete(int id)
        {
            var existingAudio = await Database.Audio.GetById(id);
            if (existingAudio == null)
            {
                throw new Exception($"Audio with id {id} not found.");
            }

            await Database.Audio.Delete(id);
            await Database.Save();
        }

        public async Task<List<AudioDTO>> GetSongsByGenreAsync(string genreName)
        {
            var audios = await Database.Audio.GetSongsByGenre(genreName);

            return audios.Select(audio => new AudioDTO
            {
                Id = audio.Id,
                Path = audio.Path,
                Title = audio.Title,
                Author = audio.Author,
                ImageId = audio.ImageId,
            }).ToList();
        }
    }
	
}
