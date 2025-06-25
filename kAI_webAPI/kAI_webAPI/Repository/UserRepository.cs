using kAI_webAPI.Dtos.User;
using kAI_webAPI.Interfaces;
using kAI_webAPI.Models.User;
using kAI_WebAPI.Services;
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
        private readonly ApplicationDBContext _context;
        private readonly IPasswordHasherService _hasher;

        public UserRepository(ApplicationDBContext context, IPasswordHasherService hasher)
        {
            _context = context;
            _hasher = hasher;
        }

        public async Task<User?> AddUserSync(User userModel)
        {
            await _context.Users.AddAsync(userModel);
            await _context.SaveChangesAsync();
            return userModel;
        }

        public Task AddUserAsync(User u)
        {
            _context.Users.Add(u);
            return Task.CompletedTask;
        }

        public Task<User?> GetByUsernameAsync(string username) =>
            _context.Users.SingleOrDefaultAsync(u => u.username == username);

        public Task SaveChangesAsync() => _context.SaveChangesAsync();

        public async Task<User?> LoginUserSync(UserLoginDto userLoginDto)
        {
            // Chỉ lấy user theo username, KHÔNG kiểm tra password ở đây
            return await _context.Users.FirstOrDefaultAsync(u => u.username == userLoginDto.username);
        }

        private bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(Convert.FromBase64String(storedSalt)))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(computedHash) == storedHash;
            }
        }

        public async Task<User?> DeleteUserSync(int id_user)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.id_users == id_user);
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
                users = users.Where(u => u.username.ToLower().Contains(query.Username.ToLower()));
            }
            if (!string.IsNullOrEmpty(query.Fullname))
            {
                users = users.Where(u => u.fullname.ToLower().Contains(query.Fullname.ToLower()));
            }
            if (!string.IsNullOrEmpty(query.Email))
            {
                users = users.Where(u => u.email.ToLower().Contains(query.Email.ToLower()));
            }
            if (!string.IsNullOrEmpty(query.Phone))
            {
                users = users.Where(u => u.phone.Contains(query.Phone));
            }
            if (!string.IsNullOrEmpty(query.Address))
            {
                users = users.Where(u => u.address.ToLower().Contains(query.Address.ToLower()));
            }
            if (!string.IsNullOrEmpty(query.SortBy))
            {
                switch (query.SortBy.ToLower())
                {
                    case "username":
                        users = query.IsDescending ? users.OrderByDescending(u => u.username) : users.OrderBy(u => u.username);
                        break;
                    case "fullname":
                        users = query.IsDescending ? users.OrderByDescending(u => u.fullname) : users.OrderBy(u => u.fullname);
                        break;
                    case "address":
                        users = query.IsDescending ? users.OrderByDescending(u => u.address) : users.OrderBy(u => u.address);
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
            var userModel = await _context.Users.FirstOrDefaultAsync(u => u.id_users == id_user);
            if (userModel == null)
            {
                return null;
            }
            return userModel;
        }

        public async Task<User?> UpdateUserSync(int id_user, UpdateUserRequestDto updateDto)
        {
            var userModel = await _context.Users.FirstOrDefaultAsync(u => u.id_users == id_user);
            if (userModel == null)
            {
                return null;
            }
            userModel.username = updateDto.username;

            // Sử dụng service băm mật khẩu
            if (!string.IsNullOrEmpty(updateDto.password))
            {
                var (hash, salt) = _hasher.HashPassword(updateDto.password);
                userModel.password_hash = hash;
                userModel.password_salt = salt;
            }

            userModel.fullname = updateDto.fullname;
            userModel.email = updateDto.email;
            userModel.phone = updateDto.phone;
            userModel.address = updateDto.address;
            await _context.SaveChangesAsync();
            return userModel;
        }
    }
}
