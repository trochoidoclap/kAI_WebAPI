using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kAI_webAPI.Models.User
{
    public class User
    {
        [Key]
        public int Id_users { get; set; }
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(50)")]
        public string Fullname { get; set; } = string.Empty;
        [StringLength(50)]
        public string Email { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(15)")]
        public string Phone { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(100)")]
        public string Address { get; set; } = string.Empty;
        public string Password_hash { get; set; } = string.Empty;
        public string Password_salt { get; set; } = string.Empty;


    }
}
