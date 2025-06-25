using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace kAI_webAPI.Dtos.User
{
    public class UserDto
    {
        public int id_users { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        public string username { get; set; } = string.Empty;
        //public string Password { get; set; } = string.Empty; // Password không cần thiết trong DTO trả về
        //[StringLength(50)] không cần thiết trong DTO trả về
        [Required(ErrorMessage = "Fullname is required.")]
        public string fullname { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string email { get; set; } = string.Empty;
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string phone { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
    }
}
