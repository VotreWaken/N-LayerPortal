using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models.AccountModels;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.ModelsDTO;
using MusicPortal.BLL.Services;
using MusicPortal.DAL.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using MultilingualSite.Filters;

namespace MusicPortal.Controllers
{
    [Culture]
    public class AccountController : Controller
    {
        // Services 
        private readonly IAccountService _accountService;
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly ISongService _songService;

        // Constructor 
        public AccountController(IAccountService AccountService, IImageService ImageService, IWebHostEnvironment AppEnvironment, ISongService songService)
        {
            _accountService = AccountService;
            _imageService = ImageService;
            _appEnvironment = AppEnvironment;
            _songService = songService;
        }

        // SignUp
        [HttpGet]
        public ActionResult SignUp() => View();

        // SignUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUp model, IFormFile imageAvatar)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var existingUser = await _accountService.GetByLogin(model.Login);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "This login is taken!");
                    return View(model);
                }

                await CreateUser(model.Login, model.Password, imageAvatar);

                return RedirectToAction("SignIn");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while signing up.");
                return View(model);
            }
        }

        // Save Account Image
        private async Task<int> SaveImage(IFormFile image)
        {
            var imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var imagePath = Path.Combine(_appEnvironment.WebRootPath, "images", imageName);

            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var createdImage = await _imageService.Create(new ImageDTO { Path = "/images/" + imageName });
            return createdImage.Id;
        }

        // Create User
        private async Task CreateUser(string login, string password, IFormFile imageAvatar)
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


        [HttpGet]
        public ActionResult SignIn() => View();

        // GET: AccountController/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignIn Model)
        {
            try
            {
                if ((await _accountService.GetAll()).Count == 0)
                    throw new InvalidOperationException();

                if (!ModelState.IsValid)
                    return View(Model);

                return await AuthenticateUser(Model);
            }
            catch (InvalidOperationException ex)
            {
                return RedirectToAction("SignUp", "Account");
            }
        }

        // Login User
        private async Task<IActionResult> AuthenticateUser(SignIn Model)
        {
            var userDTO = await _accountService.GetByLogin(Model.Login);

            if (userDTO == null)
            {
                ModelState.AddModelError("", "User not found!");
                return View(Model);
            }

            var isPasswordCorrect = await _accountService.ValidateUserPassword(userDTO, Model.Password);

            if (!isPasswordCorrect)
            {
                ModelState.AddModelError("", "Incorrect login or password!");
                return View(Model);
            }

            HttpContext.Session.SetString("Login", userDTO.Login);

            var imageDTO = await _imageService.GetById(userDTO.ImageId);
            string imagePath = imageDTO.Path;


            // Авторизация 

            // Конец


            HttpContext.Session.SetString("UserId", userDTO.Id.ToString());
            HttpContext.Session.SetString("UserImage", imagePath);
            HttpContext.Session.SetString("IsAdmin", userDTO.IsAdmin.ToString());
            HttpContext.Session.SetString("IsAuth", userDTO.IsAuth.ToString());
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ConfirmUsers()
        {
            var model = new ConfirmUsers();
            model.Users = (await _accountService.GetAll()).Select(userDTO =>
                new User
                {
                    Id = userDTO.Id,
                    Login = userDTO.Login,
                    ImageId = userDTO.ImageId,
                    IsAdmin = userDTO.IsAdmin,
                    IsAuth = userDTO.IsAuth,
                }).ToList();

            model.ImagePaths = new Dictionary<int, string>();
            foreach (var user in model.Users)
            {
                if (user.ImageId > 0)
                {
                    var imageDTO = await _imageService.GetById(user.ImageId);
                    string imagePath = imageDTO.Path;
                    model.ImagePaths.Add(user.Id, imagePath);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> AuthUser(int id)
        {
            Console.WriteLine(id);
            var userDTO = await _accountService.GetById(id);
            if (userDTO == null)
            {
                return NotFound();
            }

            userDTO.IsAuth = true;

            await _accountService.Update(userDTO);

            var usersDTO = await _accountService.GetAll();

            var users = usersDTO.Select(u => new User { Id = u.Id, Login = u.Login, IsAuth = u.IsAuth, IsAdmin = u.IsAdmin, ImageId = u.ImageId }).ToList();

            var model = new ConfirmUsers
            {
                Users = users,
                User = new User { Id = userDTO.Id, Login = userDTO.Login, ImageId = userDTO.ImageId, IsAuth = userDTO.IsAuth, IsAdmin = userDTO.IsAdmin }, // Преобразование пользователя из DTO в модель представления
                Id = id,
                ImagePaths = new Dictionary<int, string>()
            };

            foreach (var user in model.Users)
            {
                if (user.ImageId > 0)
                {
                    var imageDTO = await _imageService.GetById(user.ImageId);
                    string imagePath = imageDTO.Path;
                    model.ImagePaths.Add(user.Id, imagePath);
                }
            }
            return Json(new { success = true, userId = id });
            // return View("~/Views/Account/ConfirmUsers.cshtml", model);
        }

        // GET: AccountController
        public async Task<IActionResult> Index(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await _accountService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            var userDto = new UserIndexViewModel
            {
                User = new UserDTO
                {
                    Login = user.Login,
                    ImageId = user.ImageId
                }
            };

            // Get User Image
            var imageDto = await _imageService.GetById(user.ImageId);
            HttpContext.Session.SetString("imagePath", imageDto.Path);

            // Get User Songs
            userDto.audio = await _songService.GetSongsByUserAsync(user.Login);

            return View(userDto);
        }

        // GET: AccountController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _accountService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = await _accountService.GetById(user.Id);
                    if (existingUser == null)
                    {
                        return NotFound();
                    }

                    existingUser.Login = user.Login;
                    // existingUser.Password = user.Password;
                    existingUser.IsAdmin = user.IsAdmin;

                    await _accountService.Update(existingUser);

                    return RedirectToAction("ConfirmUsers");
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            }
            return View(user);
        }

        // GET: AccountController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            await _accountService.Delete(id);
            return Json(new { success = true, userId = id });
        }

        // Change Culture

        public ActionResult ChangeCulture(string lang)
        {
            try
            {
                string? returnUrl = HttpContext.Session.GetString("path") ?? "/Home";
                // Список культур
                List<string> cultures = new List<string>() { "ru", "en", "ua", "fr" };
                if (!cultures.Contains(lang))
                {
                    lang = "en";
                }
                Console.WriteLine($"Current Language - {lang}");
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(10); // срок хранения куки - 10 дней
                Response.Cookies.Append("lang", lang, option); // создание куки
                return Redirect(returnUrl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
