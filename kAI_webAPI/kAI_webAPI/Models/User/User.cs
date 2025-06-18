using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace kAI_webAPI.Models.User
{
    public class User
    {
        [Key]
        public int Id_users { get; set; }
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;
        [StringLength(50)]
        [Required]
        //public string Password { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        [StringLength(50)]
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        [StringLength(50)]
        public string Address { get; set; } = string.Empty;
        public string Password_hash { get; set; } = string.Empty;
        public string Password_salt { get; set; } = string.Empty;
    }
}
