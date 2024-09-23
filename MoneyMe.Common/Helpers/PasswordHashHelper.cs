using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace MoneyMe.Common.Helpers
{
    public static class PasswordHashHelper
    {
        private const int _keySize = 64;
        private const int _iterations = 350000;
        private static readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA256;
        private static readonly IConfiguration _configuration;

        static PasswordHashHelper()
        {
            _configuration = ConfigurationHelper.GetConfigurationSection("AuthDetails");
        }


        public static string HashPasword(string password)
        {
            byte[] salt = Encoding.UTF8.GetBytes(_configuration["Hash-SHA256-Key"].ToString());
            return GeneratePasswordHash(password, salt);
        }

        public static bool VerifyPassword(string password)
        {
            // This is to check if re-hashing the inputed password at login will get the 
            // same hashed password in the HashPassword method above also saved in Database:
            byte[] salt = Encoding.UTF8.GetBytes(_configuration["Hash-SHA256-Key"].ToString());
            byte[] hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, _iterations, _hashAlgorithm, _keySize);

            return hashToCompare.SequenceEqual(Convert.FromHexString(_configuration["PasswordHash"].ToString()));
        }


        private static string GeneratePasswordHash(string password, byte[] salt)
        {
            // Using Password-Based Key Derivation Function 2 (PBKDF2) for password hashing mechanism:
            byte[] passwordHash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                _iterations,
                _hashAlgorithm,
                _keySize);

            return Convert.ToHexString(passwordHash);
        }
    }
}
