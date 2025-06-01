using System.ComponentModel.DataAnnotations;

namespace kAI_webAPI.Models
{
    public class Users
    {
        [Key]
        public int Id_users { get; set; }
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;
        [StringLength(50)]
        public string Password { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
        [StringLength(50)]
        public string Email { get; set; } = string.Empty;
        public int Phone { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
