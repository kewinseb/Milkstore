using System.Security.Cryptography;

namespace MilkStore
{
    public static class EncryptionHelper
    {
        // Option to generate a secure key dynamically
        public static string GenerateSecureKey()
        {
            byte[] keyBytes = new byte[32]; // 32 bytes = 256 bits
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(keyBytes);
            }
            return Convert.ToBase64String(keyBytes); // Base64 encoding for easy storage and use
        }

        // Example: Pre-generated secure key for AES-256 (use GenerateSecureKey once and save the result securely)
        private static readonly string Key = GenerateSecureKey();

        private static byte[] GetAesKey(string base64Key)
        {
            byte[] keyBytes = Convert.FromBase64String(base64Key);
            if (keyBytes.Length != 32) throw new ArgumentException("Key must be 256 bits (32 bytes).");
            return keyBytes;
        }

        public static string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = GetAesKey(Key);
                aes.GenerateIV();
                byte[] iv = aes.IV;

                using (var encryptor = aes.CreateEncryptor(aes.Key, iv))
                using (var ms = new MemoryStream())
                {
                    ms.Write(iv, 0, iv.Length);
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var writer = new StreamWriter(cs))
                    {
                        writer.Write(plainText);
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string Decrypt(string encryptedText)
        {
            byte[] buffer = Convert.FromBase64String(encryptedText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = GetAesKey(Key);
                byte[] iv = new byte[aes.BlockSize / 8];
                Array.Copy(buffer, 0, iv, 0, iv.Length);
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream(buffer, iv.Length, buffer.Length - iv.Length))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var reader = new StreamReader(cs))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}