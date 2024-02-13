using Microsoft.AspNetCore.Mvc;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.ModelsDTO;
using MusicPortal.BLL.Services;
using MusicPortal.DAL.Entities;

namespace AdminWebAPI.Controllers
{
	[ApiController]
	[Route("api/account")]
	public class AccountController : Controller
	{
		private readonly IAccountService _accountService;
		private readonly IImageService _imageService;
		private readonly IWebHostEnvironment _appEnvironment;

		public AccountController(IAccountService AccountService, IImageService ImageService, IWebHostEnvironment AppEnvironment)
		{
			_accountService = AccountService;
			_imageService = ImageService;
			_appEnvironment = AppEnvironment;
		}

		// Get All Users
		[HttpGet("GetAll")]
		public async Task<IEnumerable<User>> GetUsers()
		{
			var users = (await _accountService.GetAll()).Select(userDTO =>
			new User
			{
				Id = userDTO.Id,
				Login = userDTO.Login,
				ImageId = userDTO.ImageId,
				IsAdmin = userDTO.IsAdmin,
				IsAuth = userDTO.IsAuth,
			}).ToList();

			return users;
		}

		// Get User
		[HttpGet("Get")]
		public async Task<ActionResult<User>> GetUser(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}
			var user = await _accountService.GetById(id);
			var userDto = new User
			{
                Id = user.Id,
                Login = user.Login,
                ImageId = user.ImageId,
                IsAdmin = user.IsAdmin,
                IsAuth = user.IsAuth,
            };
			return userDto;
		}

		// Add User 
		[HttpPost("Create")]
		public async Task CreateUser(string login, string password, IFormFile imageAvatar)
		{
			var imageId = await SaveImage(imageAvatar);

			var imageDto = new ImageDTO { Id = imageId };

			var userDto = new UserDTO
			{
				Login = login,
				Password = password,
				Image = imageDto,
				ImageId = imageDto.Id,
            };

			await _accountService.Create(userDto);
		}
		
		// Save User Image
		private async Task<int> SaveImage(IFormFile image)
		{
			var imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var imagePath = Path.Combine("images", imageName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
			{
				await image.CopyToAsync(stream);
			}

			var createdImage = await _imageService.Create(new ImageDTO { Path = "/images/" + imageName });
			return createdImage.Id;
		}
		// Edit User
		[HttpPut("Update")]
		public async Task<IActionResult> Update(int id, string login, string password, IFormFile imageAvatar)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			var existingUser = await _accountService.GetById(id);
			if (existingUser == null)
			{
				return NotFound();
			}

			existingUser.Login = login;
			existingUser.Password = password;

			if (imageAvatar != null)
			{
				var imageId = await SaveImage(imageAvatar);
				existingUser.ImageId = imageId;
			}

			await _accountService.Update(existingUser);

			return Ok();
		}

		// Delete User 
		[HttpDelete("Delete/{id}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			if (id == 0)
			{
				return BadRequest();
			}

			var existingUser = await _accountService.GetById(id);
			if (existingUser == null)
			{
				return NotFound();
			}

			await _accountService.Delete(id);

			return Ok();
		}
	}
}
