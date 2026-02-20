using System.Security.Cryptography;
using System.Text;

namespace CryptService.Services
{
    public class AESCryptService
    {
        private readonly string secretKey;

        public AESCryptService(IConfiguration configuration)
        {
            secretKey = configuration["CryptSettings:SecretKey"] ?? throw new ArgumentNullException("Secret key is missing!");

            if(secretKey.Length != 32)
            {
                throw new ArgumentException("Secret must be exactly 32 characters.");
            }
        }

        public string Encrypt(string plainText)
        {
            using Aes aesAlgorithm = Aes.Create();
            aesAlgorithm.Key = Encoding.UTF8.GetBytes(secretKey);
            aesAlgorithm.GenerateIV();

            ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor(aesAlgorithm.Key, aesAlgorithm.IV);

            using var msEncrypt = new MemoryStream();
            msEncrypt.Write(aesAlgorithm.IV, 0, aesAlgorithm.IV.Length);

            using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            using (var swEncrypt = new StreamWriter(csEncrypt))
            {
                swEncrypt.Write(plainText);
            }
            
            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        public string Decrypt(string cipherText)
        {
            byte[] fullCipher = Convert.FromBase64String(cipherText);

            using Aes aesAlgorithm = Aes.Create();
            aesAlgorithm.Key = Encoding.UTF8.GetBytes(secretKey);

            byte[] iv = new byte[16];
            byte[] cipher = new byte[fullCipher.Length - 16];

            Array.Copy(fullCipher, iv, 16);
            Array.Copy(fullCipher, 16, cipher, 0, cipher.Length);

            aesAlgorithm.IV = iv;

            ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor(aesAlgorithm.Key, aesAlgorithm.IV);

            using var msDecrypt = new MemoryStream(cipher);
            using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);

            return srDecrypt.ReadToEnd();
        }
    }
}
