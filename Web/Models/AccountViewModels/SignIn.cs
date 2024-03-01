using System.ComponentModel.DataAnnotations;
using System.Resources;

namespace MusicPortal.Models.AccountModels
{
    public class SignIn
    {
        //[Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 //ErrorMessageResourceName = "Name")]
        //[Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        public string? Login { get; set; }

        //[Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 //ErrorMessageResourceName = "Password")]
        //[Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
