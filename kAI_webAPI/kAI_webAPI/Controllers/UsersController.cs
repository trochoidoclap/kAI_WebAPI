using kAI_webAPI.Data;
using kAI_webAPI.Dtos.User;
using kAI_webAPI.Helpers;
using kAI_webAPI.Interfaces;
using kAI_webAPI.Mappers;
using kAI_webAPI.Models;
using kAI_webAPI.Models.Subjects;
using kAI_webAPI.Models.User;
using kAI_WebAPI.Services;
using kAI_webAPI.Utils;
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
using System.Security.Cryptography;
using System.Text;

namespace kAI_webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasherService _hasher;
        private readonly AppSetting _appSettings;

        public UsersController(
            ApplicationDBContext context, 
            IUserRepository userRepo,
            IPasswordHasherService hasher,
            IOptionsMonitor<AppSetting> optionsMonitor
            )
        {
            _context = context;
            _userRepo = userRepo;
            _hasher = hasher;
            _appSettings = optionsMonitor.CurrentValue;
        }
        private async Task<TokenModel> GenerateToken(UserDto userDto)
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
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), 
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var accessToken = jwtTokenHandler.WriteToken(token);
            var refreshToken = GenerateRefreshToken();
            var refreshTokenEntity = new RefreshToken
            {
                Id = Guid.NewGuid(),
                Id_users = userDto.Id_users,
                Token = refreshToken,
                JwtId = token.Id,
                IsUsed = false,
                IsRevoked = false,
                IssuedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddDays(30), // Refresh token expires in 30 days
            };
            await _context.AddAsync(refreshTokenEntity);
            await _context.SaveChangesAsync();

            return new TokenModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
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

            // Fix: Ensure the UserRegisterDto contains a Password property, as the User model does not have one.
            var (hash, salt) = _hasher.HashPassword(userDto.Password); // Use userDto.Password instead of userModel.Password
            userModel.Password_hash = hash; // Lưu trữ mật khẩu đã được băm
            userModel.Password_salt = salt; // Lưu trữ muối để băm mật khẩu
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
            var user = await _userRepo.LoginUserSync(userLoginDto); // Pass the entire UserLoginDto object instead of just the username
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            // Debug: kiểm tra giá trị hash/salt
            if (string.IsNullOrEmpty(user.Password_hash) || string.IsNullOrEmpty(user.Password_salt))
            {
                return StatusCode(500, "Hash hoặc salt bị thiếu trong DB.");
            }
            if (!_hasher.Verify(userLoginDto.Password, user.Password_hash, user.Password_salt))
            {
                return Unauthorized("Invalid username or password.");
            }

            var userDto = user.ToUserDto();
            var token = await GenerateToken(userDto); 
            return Ok(new ApiResponse
            {
                Status = "Success",
                Message = "Login successful.",
                Data = token
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
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            // Lấy sessionId từ cookie
            var sessionId = Request.Cookies["X-Session-Id"];
            if (!string.IsNullOrEmpty(sessionId))
            {
                Logger.EndSession(sessionId);
            }

            // Xóa cookie session nếu muốn
            Response.Cookies.Delete("X-Session-Id");

            return Ok(new { message = "Logged out and session log finalized." });
        }
        [HttpPost("RenewToken")]
        public async Task<IActionResult> RenewToken([FromBody] TokenModel tokenModel)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var secretKeyBytes = Encoding.UTF8.GetBytes(_appSettings.SecretKey);
            var tokenValidateParam = new TokenValidationParameters
            {
                // Tự cấp token
                ValidateIssuer = false,
                ValidateAudience = false,

                // ký vào token
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(secretKeyBytes),

                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false, // ko kiểm tra hết hạn
            };
            try
            {
                // check 1 : AccessToken valid format
                var tokenInVerification = jwtTokenHandler.ValidateToken(tokenModel.AccessToken, 
                    tokenValidateParam, out var validatedToken);

                //check 2 : Check alg
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, 
                        StringComparison.InvariantCultureIgnoreCase);
                    if (!result) // false
                    {
                        return Ok(new ApiResponse
                        {
                            Status = "Error",
                            Message = "Invalid token"
                        });
                    }
                }
                //check 3 : Check accessToken expire?
                var utcExpireDate = long.Parse(tokenInVerification.Claims.FirstOrDefault(x => 
                    x.Type == JwtRegisteredClaimNames.Exp)?.Value ?? "0");
                var expireDate = ConvertUnixTimeToDateTime(utcExpireDate);
                if (expireDate > DateTime.UtcNow)
                {
                    return Ok(new ApiResponse
                    {
                        Status = "Error",
                        Message = "Access token is still valid."
                    });
                }
                //check 4 : Check refreshToken exist in DB
                var storedToken = _context.RefreshTokens.FirstOrDefault(x => 
                    x.Token == tokenModel.RefreshToken);
                if (storedToken == null)
                {
                    return Ok(new ApiResponse
                    {
                        Status = "Error",
                        Message = "Refresh token does not exist."
                    });
                }

                //check 5 : Check refreshToken is used/revoked?
                if (storedToken.IsUsed)
                {
                    return Ok(new ApiResponse
                    {
                        Status = "Error",
                        Message = "Refresh token has already been used."
                    });
                }
                if (storedToken.IsRevoked)
                {
                    return Ok(new ApiResponse
                    {
                        Status = "Error",
                        Message = "Refresh token has been revoked."
                    });
                }

                //check 6 : AccessToken id == JwtId in refreshToken?
                var jti = tokenInVerification.Claims.FirstOrDefault(x => 
                    x.Type == JwtRegisteredClaimNames.Jti)?.Value;
                if (storedToken.JwtId != jti)
                {
                    return Ok(new ApiResponse
                    {
                        Status = "Error",
                        Message = "Invalid token ID."
                    });
                }

                // Update token is used
                storedToken.IsUsed = true;
                storedToken.IsRevoked = true;
                _context.Update(storedToken);
                await _context.SaveChangesAsync();
                // create new token
                var user = await _userRepo.GetUserByIdSync(int.Parse(tokenInVerification.Claims.FirstOrDefault(x => 
                x.Type == ClaimTypes.NameIdentifier)?.Value ?? "0"));
                if (user == null)
                {
                    return NotFound(new ApiResponse
                    {
                        Status = "Error",
                        Message = "User not found."
                    });
                }

                var userDto = user.ToUserDto();
                var token = await GenerateToken(userDto);
                return Ok(new ApiResponse
                {
                    Status = "Success",
                    Message = "Token renewed successfully.",
                    Data = token
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse
                {
                    Status = "Error",
                    Message = "Something went wrong"
                });
            }
        }

        private DateTime ConvertUnixTimeToDateTime(long utcExpireDate)
        {
            var dateTimeInterval = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            dateTimeInterval.AddSeconds(utcExpireDate).ToUniversalTime();

            return dateTimeInterval;
        }
    }
    }
