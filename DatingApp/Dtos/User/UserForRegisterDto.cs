using System.ComponentModel.DataAnnotations;

namespace DatingApp.Dtos.User
{
    public class UserForRegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [StringLength(8,MinimumLength = 4 , ErrorMessage="password length should be at least 4 and maximum 8")]
        public string Password { get; set; }
    }
}