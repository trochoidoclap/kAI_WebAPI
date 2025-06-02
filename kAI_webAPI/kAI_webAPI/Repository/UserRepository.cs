using kAI_webAPI.Dtos.User;
using kAI_webAPI.Interfaces;
using kAI_webAPI.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace kAI_webAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly Usercontext _context; // Ensure 'DataContext' is defined in the 'kAI_webAPI.Data' namespace
        public UserRepository(Usercontext context)
        {
            _context = context;
        }

        public async Task<User?> AddUserSync(User userModel)
        {
            await _context.Users.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }

        public async Task<User?> DeleteUserSync(int id_user)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id_users == id_user);
            if (user == null)
            {
                return null;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>?> GetAllUserSync()
        {
            var users = await _context.Users.ToListAsync();
            if (users == null || users.Count == 0)
            {
                return null;
            }
            return users;
        }

        public async Task<User?> GetUserByIdSync(int id_user)
        {
            var userModel = await _context.Users.FirstOrDefaultAsync(u => u.Id_users == id_user);
            if (userModel == null)
            {
                return null;
            }
            return userModel;
        }

        public async Task<User?> UpdateUserSync(int id_user, UpdateUserRequestDto updateDto)
        {
            var userModel = await _context.Users.FirstOrDefaultAsync(u => u.Id_users == id_user);
            if (userModel == null)
            {
                return null;
            }
            userModel.Username = updateDto.Username;
            userModel.Password = updateDto.Password;
            userModel.Fullname = updateDto.Fullname;
            userModel.Email = updateDto.Email;
            userModel.Phone = updateDto.Phone;
            userModel.Address = updateDto.Address;
            await _context.SaveChangesAsync();
            return userModel;
        }
    }
}
