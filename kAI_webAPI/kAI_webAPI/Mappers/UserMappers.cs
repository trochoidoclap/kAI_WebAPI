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
            // Fix: Map Password to Password_hash since User does not have a Password property
            return new User
            {
                Username = userDto.Username,
                Password_hash = userDto.Password, // Corrected mapping
                Fullname = userDto.Fullname,
                Email = userDto.Email,
                Phone = userDto.Phone,
                Address = userDto.Address
            };
        }
    }
}