using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.ModelsDTO;
using MusicPortal.BLL.Services;
using MusicPortal.Models.GenreModels;
using MusicPortal.Models.HomeModels;
using MusicPortal.Models.SongsModels;

namespace MusicPortal.Controllers
{
	public class SongsController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		private readonly ISongService _songService;

		private readonly IGenreService _genreService;

		private readonly IAccountService _accountService;

		private readonly IImageService _imageService;

		private readonly IAudioGenreService _audioGenreService;

		private readonly IWebHostEnvironment _appEnvironment;

        private readonly ISongService _audioService;
        public SongsController(ISongService SongService, IAccountService AccountService, IGenreService GenreService, IImageService ImageService, IAudioGenreService _AudioGenreService, IWebHostEnvironment appEnvironment, ISongService audioService)
        {
            _songService = SongService;
            _genreService = GenreService;
            _accountService = AccountService;
            _imageService = ImageService;
            _audioGenreService = _AudioGenreService;
            _appEnvironment = appEnvironment;
            _audioService = audioService;
        }

        [HttpGet]
        public async Task<IActionResult> Search()
        {
            var genres = await _genreService.GetAll();
            var genreSongs = new List<HomeAudioGenreModel>();

            foreach (var genre in genres)
            {
                var songs = await _audioService.GetSongsByGenreAsync(genre.Name);
                var images = new List<ImageDTO>();

                foreach (var song in songs)
                {
                    var image = await _imageService.GetById(song.ImageId);
                    images.Add(image);
                }

                var genreSongModel = new HomeAudioGenreModel
                {
                    Genre = genre,
                    Songs = songs,
                    ImagePaths = images.Select(img => img.Path).ToList()
                };
                genreSongs.Add(genreSongModel);
            }

            var viewModel = new List<HomeAudioGenreModel>(genreSongs);
            return View(viewModel);
        }

        [HttpGet]
		public async Task<IActionResult> Create()
		{
			var genres = await _genreService.GetAll();

			if (genres == null)
			{
				return NotFound();
			}

			ViewBag.AllGenres = genres
				.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name })
				.ToList();

			return View();
		}

		[HttpPost]
        public async Task<IActionResult> Create(CreateAudio model, IFormFile Path, IFormFile ImagePath)
		{
			var genres = await _genreService.GetAll();
			ViewBag.AllGenres = genres
				.Select(g => new SelectListItem { Value = g.Id.ToString(), Text = g.Name })
				.ToList();

			if (Path != null)
			{
				string audioPath = "/audio/" + Path.FileName;
				using (FileStream filestream = new FileStream(_appEnvironment.WebRootPath + audioPath, FileMode.Create))
				{
					await Path.CopyToAsync(filestream);
				}

				model.Path = audioPath;
			}

			string path = "/images/" + ImagePath.FileName;
			using (FileStream filestream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
			{
				await ImagePath.CopyToAsync(filestream);
			}

			var imageDto = new ImageDTO
			{
				Path = path
			};
			var createdImage = await _imageService.Create(imageDto);
			var imageId = createdImage.Id;

			var song = new AudioPath
			{
				Name = model.Name,
				UserId = 4,
				ImageId = imageId,
				Path = model.Path
			};

			var user = await _accountService.GetById(model.UserId);

			var audioDto = new AudioDTO
			{
				Title = model.Name,
				Author = user.Login,
				ImageId = imageId,
				Path = model.Path
			};


            audioDto = await _songService.Create(audioDto);



			if (model.SelectedGenres != null && model.SelectedGenres.Any())
			{
				foreach (var genreId in model.SelectedGenres)
				{
					var audioGenre = new AudioGenreDTO
					{
						AudioId = audioDto.Id,
						GenreId = genreId
					};
					await _audioGenreService.Create(audioGenre);
				}
			}

			return RedirectToAction("Create");
		}
	}
}
