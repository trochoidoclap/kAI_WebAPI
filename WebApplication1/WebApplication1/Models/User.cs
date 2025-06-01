using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaiApi.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id_users")]
        public int Id { get; set; }

        [Required]
        [Column("username", TypeName = "varchar(50)")]
        public string Username { get; set; } = null!;

        [Required]
        [Column("password", TypeName = "varchar(50)")]
        public string Password { get; set; } = null!;

        [Required]
        [Column("fullname", TypeName = "text")]
        public string Fullname { get; set; } = null!;

        [Required]
        [Column("email", TypeName = "varchar(50)")]
        public string Email { get; set; } = null!;

        [Required]
        [Column("phone")]
        public uint Phone { get; set; }

        [Required]
        [Column("address", TypeName = "text")]
        public string Address { get; set; } = null!;

        // Quan hệ 1-N với bảng transcript
        public ICollection<Transcript> Transcripts { get; set; } = new List<Transcript>();
    }
}
