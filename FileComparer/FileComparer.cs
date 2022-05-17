using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace FileComparer
{
    public class FileComparer
    {
        public bool IsEqual(string path1, string path2)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] file1Hash = sha256.ComputeHash(getBytes(path1));
            byte[] file2Hash = sha256.ComputeHash(getBytes(path2));
            if (file1Hash.Length != file2Hash.Length)
                return false;
            for (int i = 0; i < file1Hash.Length; i++)
            {
                if (file1Hash[i] != file2Hash[i])
                    return false;
            }
            return true;
        }

        private byte[] getBytes(string path)
        {
            using StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open));
            string content = sr.ReadToEnd();
            return Encoding.UTF8.GetBytes(content);
        }
    }
}