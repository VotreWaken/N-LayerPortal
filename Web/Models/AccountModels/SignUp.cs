using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models.AccountModels
{
    public class SignUp
    {
        [Required(ErrorMessage = "обязательное поле")]
        // [Remote(action: "CheckLogin", controller: "Account", ErrorMessage = "Логин уже используется")]
        // [RegularExpression(@"[^!@#$%^&*()\\\.\-=[\]{}/<>~+|]{1,100}", ErrorMessage = "Некорректные данные")]
        public string? Login { get; set; }
        public string? Email { get; set; }

        [Required(ErrorMessage = "обязательное поле")]
        // [RegularExpression(@"[$A-Z]+[A-Za-z0-9]{5,100}", ErrorMessage = "Некорректные данные")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
		public string? ImageAvatar { get; set; }

		[Required(ErrorMessage = "обязательное поле")]
        [Compare("Password", ErrorMessage = "Passwords Might Be Same")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
