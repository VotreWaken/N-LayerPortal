using AdminWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.ModelsDTO;
using MusicPortal.DAL.Entities;
using MusicPortal.Models.SongsModels;
using static System.Net.Mime.MediaTypeNames;

namespace AdminWebAPI.Controllers
{
	[ApiController]
	[Route("api/song")]
	public class SongController : Controller
	{
		// Services 
		private readonly ISongService _songService;
		private readonly IGenreService _genreService;
		private readonly IAccountService _accountService;
		private readonly IImageService _imageService;
		private readonly IAudioGenreService _audioGenreService;
		private readonly IWebHostEnvironment _appEnvironment;

		// Constructor 
		public SongController(ISongService SongService, IAccountService AccountService, IGenreService GenreService, IImageService ImageService, IAudioGenreService _AudioGenreService, IWebHostEnvironment appEnvironment)
		{
			_songService = SongService;
			_genreService = GenreService;
			_accountService = AccountService;
			_imageService = ImageService;
			_audioGenreService = _AudioGenreService;
			_appEnvironment = appEnvironment;
		}

		// Get All Songs
		[HttpGet("GetAll")]
		public async Task<IEnumerable<Audio>> GetAllSongs()
		{
			var Songs = (await _songService.GetAll()).Select(SongsDTO =>
			new Audio
			{
				Id = SongsDTO.Id,
				Author = SongsDTO.Author,
				Title = SongsDTO.Title,
				ImageId = SongsDTO.ImageId,
				Path = SongsDTO.Path
			}).ToList();

			return Songs;
		}

		// Get Song
		[HttpGet("Get")]
		public async Task<ActionResult<Audio>> GetSong(int id)
		{
			var Song = await _songService.GetById(id);
			var SongDto = new Audio()
			{
				Id = Song.Id,
				Author = Song.Author,
				Title = Song.Title,
				ImageId = Song.ImageId,
				Path = Song.Path
			};
			return SongDto;
		}

		// Create Song
		// При таком способе отображает только модель AudioCreation
		// Принимаемые параметры IFormFile не отображаются на GUI Swagger
		//[HttpPost]
		//public async Task Create(AudioCreation model, IFormFile Path, IFormFile ImagePath)
		//{
		//	var audioPath = await SaveAudioFile(Path);

		//	var imageId = await SaveImageFile(ImagePath);

		//	var audioDto = await CreateAudioDto(model, audioPath, imageId);

		//	await _songService.Create(audioDto);

		//	await AttachGenresToAudio(model.SelectedGenres, audioDto.Id);
		//}

		[HttpPost]
		public async Task Create(AudioCreate model)
		{
			// Save Audio File
			var audioPath = await SaveAudioFile(model.SongPath);

			// Save Image
			var imageId = await SaveImageFile(model.ImagePath);

			AudioCreation song = new AudioCreation()
			{
				Id = model.Id,
				Name = model.Name,
				SelectedGenres = model.SelectedGenres,
				ImageId = imageId,
				UserId = model.UserId,
				Path = audioPath,
            };

			var audioDto = await CreateAudioDto(song, audioPath, imageId);

			await _songService.Create(audioDto);

			// Bind Genres To Audio 
			await AttachGenresToAudio(model.SelectedGenres, audioDto.Id);
		}

		// Save Audio File
		private async Task<string> SaveAudioFile(IFormFile audioFile)
		{
			if (audioFile != null)
			{
				string audioPath = Path.Combine("audio" + audioFile.FileName);
                using (FileStream filestream = new FileStream(audioPath, FileMode.Create))
				{
					await audioFile.CopyToAsync(filestream);
				}
				return audioPath;
			}
			return null;
		}

		// Save Image File
		private async Task<int> SaveImageFile(IFormFile imageFile)
        {
            if (imageFile != null)
			{
                var imageName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var imagePath = Path.Combine("images", imageName);
                using (FileStream filestream = new FileStream(imagePath, FileMode.Create))
				{
					await imageFile.CopyToAsync(filestream);
				}

				var imageDto = new ImageDTO
				{
					Path = imagePath
				};
				var createdImage = await _imageService.Create(imageDto);
				return createdImage.Id;
			}
			return -1;
		}

		// Create DTO Audio
		private async Task<AudioDTO> CreateAudioDto(AudioCreation model, string audioPath, int imageId)
		{
			var user = await _accountService.GetById(model.UserId);

			var audioDto = new AudioDTO
			{
				Title = model.Name,
				Author = user.Login,
				ImageId = imageId,
				Path = audioPath
			};
			return audioDto;
		}

		// Bind Genres To Audio 
		private async Task AttachGenresToAudio(IEnumerable<int> genreIds, int audioId)
		{
			if (genreIds != null && genreIds.Any())
			{
				foreach (var genreId in genreIds)
				{
					var audioGenre = new AudioGenreDTO
					{
						AudioId = audioId,
						GenreId = genreId
					};
					await _audioGenreService.Create(audioGenre);
				}
			}
		}

		// Edit Song
		[HttpPut]
		public async Task<IActionResult> Update(AudioCreate model)
		{
			if (model.Id == 0)
			{
				return BadRequest();
			}

			try
			{
				var existingSong = await _songService.GetById(model.Id);
				if (existingSong == null)
				{
					return NotFound();
				}

                existingSong.Author = model.UserId.ToString();
				existingSong.Path = model.Path;
				existingSong.Id = model.Id;
				existingSong.ImageId = model.ImageId;
				existingSong.Title = model.Name;

				await _songService.Update(existingSong);
			}
			catch (DbUpdateConcurrencyException)
			{
				throw;
			}
			return Ok();
		}

		// Delete Song
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			var existingUser = await _songService.GetById(id);
			if (existingUser == null)
			{
				return NotFound();
			}

			await _songService.Delete(id);

			return Ok();
		}

	}
}
