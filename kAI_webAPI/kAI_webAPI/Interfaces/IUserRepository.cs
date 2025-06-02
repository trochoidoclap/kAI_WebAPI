using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kAI_webAPI.Dtos.User;
using kAI_webAPI.Models.User;

namespace kAI_webAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> AddUserSync(User userModel);
        Task<User?> UpdateUserSync(int id_user, UpdateUserRequestDto updateDto);
        Task<User?> DeleteUserSync(int id_user);
        Task<List<User>?> GetAllUserSync();
        Task<User?> GetUserByIdSync(int id_user);
    }
}
