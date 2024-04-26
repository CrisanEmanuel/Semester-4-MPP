using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Server
{
    public static class Password
    {
        private static string HashPassword(string password)
        {
            using var sha256Hash = SHA256.Create();
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }

        // for sign up
        public static bool VerifyPassword(string inputPassword, string storedHash)
        {
            var hashedInputPassword = HashPassword(inputPassword);
            return string.Equals(hashedInputPassword, storedHash);
        }

        // Minimum 8 characters, at least one uppercase letter, one digit, and one of the characters @#$%^&+=!
        // for sign up
        public static bool CheckPasswordFormat(string password)
        {
            const string regex = "^(?=.*[A-Z])(?=.*\\d)(?=.*[@#$%^&+=!]).{8,}$";
            var pattern = new Regex(regex);
            return pattern.IsMatch(password);
        }
    }
}