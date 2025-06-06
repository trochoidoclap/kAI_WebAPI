using kAI_webAPI.Data;
using kAI_webAPI.Dtos.User;
using kAI_webAPI.Helpers;
using kAI_webAPI.Interfaces;
using kAI_webAPI.Mappers;
using kAI_webAPI.Models;
using kAI_webAPI.Models.Subjects;
using kAI_webAPI.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace kAI_webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IUserRepository _userRepo;
        private readonly AppSetting _appSettings;

        public UsersController(ApplicationDBContext context, IUserRepository userRepo, IOptionsMonitor<AppSetting> optionsMonitor)
        {
            _context = context;
            _userRepo = userRepo;
            _appSettings = optionsMonitor.CurrentValue;
        }
        private string GenerateToken(UserDto userDto)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userDto.Id_users.ToString()),
                    new Claim(ClaimTypes.Name, userDto.Username),
                    new Claim(ClaimTypes.Email, userDto.Email ?? string.Empty),
                    new Claim("Fullname", userDto.Fullname ?? string.Empty),
                    new Claim("Phone", userDto.Phone.ToString()),
                    new Claim("Address", userDto.Address ?? string.Empty),
                    // roles cos thể được thêm vào đây nếu cần
                    
                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7), // Token expires in 7 days
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        [HttpPost]
        [Route("/Users/Create")]
        public async Task<IActionResult> ThemUser([FromBody] UserRegisterDto userDto) // Dùng [FromBody] để nhận dữ liệu từ body của request
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
        [HttpPost("Login")]
        public async Task<IActionResult> DangNhap([FromBody] UserLoginDto userLoginDto)
        {
            if (userLoginDto == null)
            {
                return BadRequest("Invalid login data.");
            }
            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrEmpty(userLoginDto.Username) || string.IsNullOrEmpty(userLoginDto.Password))
            {
                return BadRequest("Username and Password are required.");
            }
            var user = await _userRepo.LoginUserSync(userLoginDto);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            var userDto = user.ToUserDto();
            return Ok(new ApiResponse
            {
                Status = "Success",
                Message = "Login successful.",
                Data = GenerateToken(userDto)
            });
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
        public async Task<IActionResult> LayTatCaUsers([FromQuery] QueryObject query)
        {
            var users = await _userRepo.GetAllUserSync(query);
            if (users == null)
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
