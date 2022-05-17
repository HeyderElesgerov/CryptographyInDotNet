using System;
using System.Security.Cryptography;
using PasswordProtection;
using FileComparer = FileComparer.FileComparer;

namespace CryptographyInDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            string password = "Heyder2002";
            string salt = "sdfkjksdfnkjsafks";
            string hash = PasswordHelper.Hash(password, salt, 1000);
            Console.WriteLine(hash);
            Console.WriteLine(PasswordHelper.IsCorrectPassword(password, salt, hash, 1000));
        }
    }
}