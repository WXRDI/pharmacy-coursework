using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace PharmacyCoursework
{
    internal class PasswordHasher
    {
        private const int SALT_SIZE = 16;
        private const int HASH_SIZE = 32;
        private const int ITERATIONS = 100000;

        public static string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SALT_SIZE);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                password, salt, ITERATIONS, HashAlgorithmName.SHA256, HASH_SIZE);
            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }

        public static bool Verify(string password, string stored) 
        {
            var parts = stored.Split('.');
            if (parts.Length != 2) return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] expected = Convert.FromBase64String(parts[1]);
            byte[] actual = Rfc2898DeriveBytes.Pbkdf2(
                password, salt, ITERATIONS, HashAlgorithmName.SHA256, HASH_SIZE);

            return CryptographicOperations.FixedTimeEquals(actual, expected);
        }
    }
}
