using System;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

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

        public static bool IsCorrectPassword(string password, string salt, string hash)
        {
            return hash == Hash(password, salt);
        }
    }
}