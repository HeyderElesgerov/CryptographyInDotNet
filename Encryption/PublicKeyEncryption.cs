using System.Security.Cryptography;

namespace Encryption
{
    public static class PublicKeyEncryption
    {
        public static void GenerateKeys(out byte[] privateKey, out byte[] publicKey)
        {
            using var rsaProvide = new RSACryptoServiceProvider();
            privateKey = rsaProvide.ExportRSAPrivateKey();
            publicKey = rsaProvide.ExportRSAPublicKey();
        }

        public static byte[] EncryptData(byte[] data, byte[] publicKey)
        {
            using var rsa = new RSACryptoServiceProvider();
            rsa.ImportRSAPublicKey(publicKey, out int _);
            return rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);
        }

        public static byte[] DecryptData(byte[] encryptedBytes, byte[] privateKey)
        {
            using var rsa = new RSACryptoServiceProvider();
            rsa.ImportRSAPrivateKey(privateKey, out int _);
            return rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.Pkcs1);
        }

        public static byte[] SignData(byte[] data, byte[] privateKey, HashAlgorithmName hashAlgorithmName)
        {
            using var rsa = new RSACryptoServiceProvider();
            rsa.ImportRSAPrivateKey(privateKey, out int _);
            byte[] hashBytes = HashAlgorithm.Create(hashAlgorithmName.Name).ComputeHash(data);
            return rsa.SignHash(hashBytes, hashAlgorithmName, RSASignaturePadding.Pkcs1);
        }

        public static bool VerifySign(byte[] originalData, byte[] digitalSign, byte[] publicKey, HashAlgorithmName hashAlgorithmName)
        {
            using var rsa = new RSACryptoServiceProvider();
            rsa.ImportRSAPublicKey(publicKey, out int _);
            return rsa.VerifyData(originalData, digitalSign, hashAlgorithmName, RSASignaturePadding.Pkcs1);
        }
    }
}