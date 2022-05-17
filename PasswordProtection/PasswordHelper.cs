using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace PasswordProtection
{
    public static class PasswordHelper
    {
        public static string Hash(string password, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password + salt);
            byte[] hashBytes = SHA256.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
        
        public static string Hash(string password, string salt, int iterationCount)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            byte[] hash = KeyDerivation.Pbkdf2(password, saltBytes, KeyDerivationPrf.HMACSHA256, iterationCount, 256 / 8);
            return Convert.ToBase64String(hash);
        }

        public static bool IsCorrectPassword(string password, string salt, string hash)
        {
            return hash == Hash(password, salt);
        }
        
        public static bool IsCorrectPassword(string password, string salt, string hash, int iterationCount)
        {
            return hash == Hash(password, salt, iterationCount);
        }
    }
}