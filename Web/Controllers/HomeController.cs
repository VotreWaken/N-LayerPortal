using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.ModelsDTO;
using MusicPortal.Models;
using MusicPortal.Models.GenreModels;
using MusicPortal.Models.HomeModels;
using System.Diagnostics;
using MultilingualSite.Filters;

namespace MusicPortal.Controllers
{
    [Culture]
    public class HomeController : Controller
    {
        // private readonly ILogger<HomeController> _logger;
        private readonly IGenreService _genreService;
        private readonly IImageService _imageService;
        private readonly ISongService _audioService;

        public HomeController(IGenreService GenreService, IImageService ImageService, ISongService AudioService)
        {
            // _logger = logger;
            this._genreService = GenreService;
            this._imageService = ImageService;
            this._audioService = AudioService;

        }

        public ActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.Session.Clear();
            return RedirectToAction("SignIn", "Account");
        }



        public async Task<IActionResult> Index()
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult ChangeCulture(string lang)
        {
            string? returnUrl = HttpContext.Session.GetString("path") ?? "/Club/Index";

            // Список культур
            List<string> cultures = new List<string>() { "ru", "en", "ua", "fr" };
            if (!cultures.Contains(lang))
            {
                lang = "ru";
            }

            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(10); // срок хранения куки - 10 дней
            Response.Cookies.Append("lang", lang, option); // создание куки
            return Redirect(returnUrl);
        }
    }
}
