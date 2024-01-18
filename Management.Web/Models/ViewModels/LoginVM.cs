using System.ComponentModel.DataAnnotations;

namespace Management.Web.Models.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
