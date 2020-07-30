using System.ComponentModel.DataAnnotations;

namespace DatingApp.Dtos.User
{
    public class UserForLoginDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}