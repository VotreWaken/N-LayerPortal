using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models.AccountModels
{
    public class SignIn
    {
        [Required(ErrorMessage = "Required Field")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Required Field")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
