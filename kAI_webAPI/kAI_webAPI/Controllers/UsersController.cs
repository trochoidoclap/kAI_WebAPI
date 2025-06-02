using kAI_webAPI.Dtos.User;
using kAI_webAPI.Mappers;
using kAI_webAPI.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.ComponentModel.DataAnnotations;

namespace kAI_webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Usercontext _context;

        public UsersController(Usercontext context)
        {
            _context = context;
        }
        [HttpPost]
        [Route("/Users/Create")]
        public IActionResult ThemUser([FromBody] CreateUserRequestDto userDto) // Dùng [FromBody] để nhận dữ liệu từ body của request
        {
            if (userDto == null)
            {
                return BadRequest("Invalid user data.");
            }
            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrEmpty(userDto.Username) || string.IsNullOrEmpty(userDto.Password) || string.IsNullOrEmpty(userDto.Fullname))
            {
                return BadRequest("Username, Password, and Fullname are required.");
            }
            var userModel = userDto.ToUserFromCreateDto();
            _context.Users.Add(userModel);
            _context.SaveChanges();
            return Ok("User created successfully.");
        }
        [HttpPut]
        [Route("/Users/Update/{id_user}")]
        public IActionResult CapnhatUser([FromRoute] int id_user, [FromBody] UpdateUserRequestDto updateDto)
        {
            var userModel = _context.Users.FirstOrDefault(u => u.Id_users == id_user);

            if (userModel == null)
            {
                return NotFound("User not found.");
            }
            userModel.Username = updateDto.Username;
            userModel.Password = updateDto.Password;
            userModel.Fullname = updateDto.Fullname;
            userModel.Email = updateDto.Email;
            userModel.Phone = updateDto.Phone;
            userModel.Address = updateDto.Address;

            _context.SaveChanges();
            return Ok("User updated successfully.");
        }

        [HttpPost]
        [Route("/Users/Delete")]
        public IActionResult XoaUser(int id_user)
        {
            if (id_user <= 0)
            {
                return BadRequest("Invalid user ID.");
            }
            var user = _context.Users.Find(id_user);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Ok("User deleted successfully.");
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList().Select(s => s.ToUserDto());
            return Ok(users);
        }
    }
}
