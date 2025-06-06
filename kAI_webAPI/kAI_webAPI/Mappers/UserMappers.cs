using System;
using System.ComponentModel.DataAnnotations;
using kAI_webAPI.Dtos.User;
using kAI_webAPI.Models.User;
using System.Linq;
using System.Threading.Tasks;
//
// Mappers dùng để chuyển đổi Model và DTO
//
namespace kAI_webAPI.Mappers
{
    public static class UserExtensions
    {
        public static UserDto ToUserDto(this User userModel) //Dto Get về từ Model User
        {
            return new UserDto
            {
                Id_users = userModel.Id_users,
                Username = userModel.Username,
                Fullname = userModel.Fullname,
                Email = userModel.Email,
                Phone = userModel.Phone,
                Address = userModel.Address
            };
        }
        public static User ToUserFromCreateDto(this UserRegisterDto userDto)
        {
            return new User
            {
                Username = userDto.Username,
                Password = userDto.Password,
                Fullname = userDto.Fullname,
                Email = userDto.Email,
                Phone = userDto.Phone,
                Address = userDto.Address
            };
        }
    }
}