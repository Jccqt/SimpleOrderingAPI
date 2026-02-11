using System.Security.Cryptography;
using System.Text;

namespace OrderingAPI.Helpers
{
    public class CustomSecurity
    {
        public static string HashPassword(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string saltedPassword = password + salt;

                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                StringBuilder builder = new StringBuilder();

                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string GenerateSalt()
        {
            byte[] saltByes = new byte[16];

            using (var random = RandomNumberGenerator.Create())
            {
                random.GetBytes(saltByes);
            }

            return Convert.ToBase64String(saltByes);
        }
    }
}
