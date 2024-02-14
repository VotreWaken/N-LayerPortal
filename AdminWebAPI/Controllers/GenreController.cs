using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.ModelsDTO;
using MusicPortal.BLL.Services;
using MusicPortal.DAL.Entities;


namespace AdminWebAPI.Controllers
{
	[ApiController]
	[Route("api/genre")]
	public class GenreController : Controller
	{
		// Services 
		private readonly IGenreService _genreService;

		// Constructor
		public GenreController(IGenreService genreService)
		{
			_genreService = genreService;
		}
		// Get All Genres
		[HttpGet("GetAll")]
		public async Task<IEnumerable<Genre>> GetAllGenres()
		{
			var genres = (await _genreService.GetAll()).Select(genreDTO =>
			new Genre
			{
				Id = genreDTO.Id,
				Name = genreDTO.Name,
			}).ToList();

			return genres;
		}
		// Get Genre
		[HttpGet("Get")]
		public async Task<ActionResult<Genre>> GetGenre(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var genre = await _genreService.GetById(id);
			var genreDTO = new Genre
			{
				Id = genre.Id,
				Name = genre.Name,
			};
			return genreDTO;
		}
		
		// Create
		[HttpPost("Create")]
		public async Task Create(string name)
		{
			GenreDTO genreDTO = new GenreDTO
			{
				Name = name
			};
			await _genreService.Create(genreDTO);
		}
		// Edit 
		[HttpPut("Edit")]
		public async Task<IActionResult> Edit(int id, string name)
		{

			GenreDTO genreDTO = await _genreService.GetById(id);


			if (genreDTO == null)
			{
				return NotFound();
			}


			GenreDTO genreModel = new GenreDTO
			{
				Id = genreDTO.Id,
				Name = name,
			};

			await _genreService.Update(genreModel);

			return Ok(genreModel);
		}
		// Delete
		[HttpDelete("Delete/{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			var existingGenre = await _genreService.GetById(id);

			if (existingGenre == null)
			{
				return NotFound();
			}

			await _genreService.Delete(id);

			return Ok();
		}
	}
}
