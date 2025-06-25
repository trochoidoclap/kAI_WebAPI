using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kAI_webAPI.Models.User
{
    public class User
    {
        [Key]
        public int id_users { get; set; }
        [StringLength(50)]
        public string username { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(50)")]
        public string fullname { get; set; } = string.Empty;
        [StringLength(50)]
        public string email { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(15)")]
        public string phone { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(100)")]
        public string address { get; set; } = string.Empty;
        public string password_hash { get; set; } = string.Empty;
        public string password_salt { get; set; } = string.Empty;


    }
}
