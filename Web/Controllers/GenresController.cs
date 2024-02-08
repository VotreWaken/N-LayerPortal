using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.ModelsDTO;
using MusicPortal.Models.AccountModels;
using MusicPortal.Models.GenreModels;

namespace MusicPortal.Controllers
{
	public class GenresController : Controller
	{
        private readonly IGenreService _genreService;

		public GenresController(IGenreService repository)
		{
			this._genreService = repository;
		}
		public async Task<IActionResult> Create(GenreModel model)
		{
            GenreDTO genreDTO = new GenreDTO
            {
                Id = model.NewGenre.Id,
                Name = model.NewGenre.Name
            };
            await _genreService.Create(genreDTO);
			return RedirectToAction("Index", "Genres");
		}

		public async Task<IActionResult> Edit(int id)
		{
            // Получаем жанр по его идентификатору
            GenreDTO genreDTO = await _genreService.GetById(id);

            // Проверяем, найден ли жанр
            if (genreDTO == null)
            {
                return NotFound();
            }

            // Создаем объект GenreModel и заполняем его данными из объекта GenreDTO
            GenreModel genreModel = new GenreModel
            {
                NewGenre = new Genre
                {
                    Id = genreDTO.Id,
                    Name = genreDTO.Name
                }
            };

            // Возвращаем представление с объектом GenreModel для редактирования
            return View(genreModel);
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, Genre genre)
		{
            GenreDTO genreDTO = new GenreDTO
            {
                Id = genre.Id,
                Name = genre.Name
            };

            // Вызываем метод Update сервиса для обновления жанра
            await _genreService.Update(genreDTO);

            // Перенаправляем пользователя на страницу с жанрами
            return RedirectToAction("Index");
        }

		public async Task<IActionResult> Delete(int id)
		{
			await _genreService.Delete(id);
			return RedirectToAction("Index");
		}

        // Доделать
		public async Task<IActionResult> Index()
		{
            var genres = await _genreService.GetAll();
            var genreDTOs = new List<GenreDTO>();

            foreach (var genre in genres)
            {
                var genreDTO = new GenreDTO
                {
                    Id = genre.Id,
                    Name = genre.Name
                };

                genreDTOs.Add(genreDTO);
            }

            GenreModel model = new GenreModel();
            model.Genres = genreDTOs;

            return View(model);
		}
	}
}
