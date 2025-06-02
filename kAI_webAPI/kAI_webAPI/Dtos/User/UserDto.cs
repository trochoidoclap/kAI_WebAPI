using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace kAI_webAPI.Dtos.User
{
    public class UserDto
    {
        //public int Id_users { get; set; } // Ko cần Id_users trong DTO trả về
        public string Username { get; set; } = string.Empty;
        //public string Password { get; set; } = string.Empty; // Password không cần thiết trong DTO trả về
        //[StringLength(50)] không cần thiết trong DTO trả về
        public string Fullname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Phone { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
