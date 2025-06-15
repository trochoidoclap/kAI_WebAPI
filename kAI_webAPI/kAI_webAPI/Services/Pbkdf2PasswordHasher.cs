using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

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