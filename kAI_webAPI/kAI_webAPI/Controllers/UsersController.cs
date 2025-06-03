using kAI_webAPI.Dtos.User;
using kAI_webAPI.Interfaces;
using kAI_webAPI.Mappers;
using kAI_webAPI.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.ComponentModel.DataAnnotations;

namespace kAI_webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Usercontext _context;
        private readonly IUserRepository _userRepo;

        public UsersController(Usercontext context, IUserRepository userRepo)
        {
            _context = context;
            _userRepo = userRepo;
        }
        [HttpPost]
        [Route("/Users/Create")]
        public async Task<IActionResult> ThemUser([FromBody] CreateUserRequestDto userDto) // Dùng [FromBody] để nhận dữ liệu từ body của request
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
            await _userRepo.AddUserSync(userModel);
            return Ok("User created successfully.");
        }
        [HttpPut]
        [Route("/Users/Update/{id_user:int}")]
        public async Task<IActionResult> CapnhatUser([FromRoute] int id_user, [FromBody] UpdateUserRequestDto updateDto)
        {
            await _userRepo.UpdateUserSync(id_user, updateDto);
            return Ok("User updated successfully.");
        }

        [HttpPost]
        [Route("/Users/Delete")]
        public async Task<IActionResult> XoaUser(int id_user)
        {
            if (id_user <= 0)
            {
                return BadRequest("Invalid user ID.");
            }
            await _userRepo.DeleteUserSync(id_user);
            return Ok("User deleted successfully.");
        }
        [HttpGet]
        [Route("/Users/GetAll")]
        public async Task<IActionResult> LayTatCaUsers()
        {
            var users = await _userRepo.GetAllUserSync();
            if (users == null) // Ensure 'users' is not null before calling Select
            {
                return NotFound("No users found.");
            }
            var userDtos = users.Select(s => s.ToUserDto());
            return Ok(userDtos);
        }
        [HttpGet]
        [Route("/Users/GetById/{id_user:int}")]
        public async Task<IActionResult> LayUserbyId_User(int id_user)
        {
            if (id_user <= 0)
            {
                return BadRequest("Invalid user ID.");
            }
            var user = await _userRepo.GetUserByIdSync(id_user);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            var userDto = user.ToUserDto();
            return Ok(userDto);
        }
    }
}
