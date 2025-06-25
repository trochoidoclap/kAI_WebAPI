using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace kAI_webAPI.Dtos.User
{
    public class UpdateUserRequestDto
    {
        public string? username { get; set; }
        public string? password { get; set; }
        public string? fullname { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? address { get; set; }
    }
}
