using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using kAI_webAPI.Interfaces;

namespace kAI_WebAPI.Services
{
    public class Pbkdf2PasswordHasher : IPasswordHasherService
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 150_000;

        public (string Hash, string Salt) HashPassword(string plain)
        {
            var saltBytes = new byte[SaltSize];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            var key = KeyDerivation.Pbkdf2(
                password: plain,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: Iterations,
                numBytesRequested: KeySize);

            return (Convert.ToBase64String(key), Convert.ToBase64String(saltBytes));
        }

        public (string hash, string salt) HashPassword(object password) // Updated tuple element names to match the interface
        {
            if (password is string plainPassword)
            {
                return HashPassword(plainPassword);
            }
            throw new ArgumentException("Password must be a string.", nameof(password));
        }

        public bool Verify(string plain, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);
            var key = KeyDerivation.Pbkdf2(
                password: plain,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: Iterations,
                numBytesRequested: KeySize);

            return Convert.ToBase64String(key) == storedHash;
        }
    }
}