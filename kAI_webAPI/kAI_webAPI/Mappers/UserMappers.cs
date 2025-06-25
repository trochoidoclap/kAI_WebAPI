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
                id_users = userModel.id_users,
                username = userModel.username,
                fullname = userModel.fullname,
                email = userModel.email,
                phone = userModel.phone,
                address = userModel.address
            };
        }
        public static User ToUserFromCreateDto(this UserRegisterDto userDto)
        {
            // Fix: Map Password to Password_hash since User does not have a Password property
            return new User
            {
                username = userDto.Username,
                password_hash = userDto.Password, // Corrected mapping
                fullname = userDto.Fullname,
                email = userDto.Email,
                phone = userDto.Phone,
                address = userDto.Address
            };
        }
    }
}