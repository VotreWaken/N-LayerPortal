using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models.AccountModels
{
    public class SignUp
    {
        [Required(ErrorMessage = "обязательное поле")]
        public string? Login { get; set; }
        public string? Email { get; set; }

        [Required(ErrorMessage = "обязательное поле")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
		public string? ImageAvatar { get; set; }

		[Required(ErrorMessage = "обязательное поле")]
        [Compare("Password", ErrorMessage = "Passwords Might Be Same")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
