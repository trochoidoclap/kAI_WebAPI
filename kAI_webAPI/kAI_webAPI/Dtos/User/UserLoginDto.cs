using System.ComponentModel.DataAnnotations;

namespace kAI_webAPI.Dtos.User
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, ErrorMessage = "Username cannot be longer than 50 characters.")]
        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        [StringLength(250, ErrorMessage = "Password cannot be longer than 250 characters.")]
        public string Password { get; set; } = string.Empty;
    }
}
