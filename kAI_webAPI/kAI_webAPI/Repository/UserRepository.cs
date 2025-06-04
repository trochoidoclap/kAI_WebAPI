using kAI_webAPI.Dtos.User;
using kAI_webAPI.Interfaces;
using kAI_webAPI.Models.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using kAI_webAPI.Helpers;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using kAI_webAPI.Data;
using Microsoft.IdentityModel.Tokens;


namespace kAI_webAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context; // Ensure 'DataContext' is defined in the 'kAI_webAPI.Data' namespace
        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<User?> AddUserSync(User userModel)
        {
            await _context.Users.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }
        public async Task<User?> LoginUserSync(UserLoginDto userLoginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == userLoginDto.Username && u.Password == userLoginDto.Password);
            if (user == null)
            {
                return null;
            }
            return user;
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

        public async Task<List<User>?> GetAllUserSync(QueryObject query)
        {
            var users = _context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(query.Username))
            {
                users = users.Where(u => u.Username.ToLower().Contains(query.Username.ToLower()));
            }
            if (!string.IsNullOrEmpty(query.Fullname))
            {
                users = users.Where(u => u.Fullname.ToLower().Contains(query.Fullname.ToLower()));
            }
            if (!string.IsNullOrEmpty(query.Email))
            {
                users = users.Where(u => u.Email.ToLower().Contains(query.Email.ToLower()));
            }
            if (!string.IsNullOrEmpty(query.Phone))
            {
                users = users.Where(u => u.Phone.Contains(query.Phone));
            }
            if (!string.IsNullOrEmpty(query.Address))
            {
                users = users.Where(u => u.Address.ToLower().Contains(query.Address.ToLower()));
            }
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                switch (query.SortBy.ToLower())
                {
                    case "username":
                        users = query.IsDescending ? users.OrderByDescending(u => u.Username) : users.OrderBy(u => u.Username);
                        break;
                    case "fullname":
                        users = query.IsDescending ? users.OrderByDescending(u => u.Fullname) : users.OrderBy(u => u.Fullname);
                        break;
                    case "address":
                        users = query.IsDescending ? users.OrderByDescending(u => u.Address) : users.OrderBy(u => u.Address);
                        break;
                    default:
                        break;
                }
            }
            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await users.Skip(skipNumber).Take(query.PageSize).ToListAsync();
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
