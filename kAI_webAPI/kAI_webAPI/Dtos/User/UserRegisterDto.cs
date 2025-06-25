using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace kAI_webAPI.Dtos.User
{
    public class UserRegisterDto
    {
        // Ko cần khai báo Id_users trong DTO tạo mới người dùng
        [Required(ErrorMessage = "Username is required")]
        public string username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Fullname is required")]
        public string fullname { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string email { get; set; } = string.Empty;
        [Phone(ErrorMessage = "Invalid phone number format")]
        public string phone { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
    }
}
