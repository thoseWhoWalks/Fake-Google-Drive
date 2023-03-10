using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace FGD.Encryption.Helpers
{
    public static class Pbkdf2KeyDerivationHelper
    {
        #region Contants
        private const int SaltSize = 128 / 8;
        private const int HashSize = 256 / 8;
        private const int HashingIterationsCount = 10000;
        #endregion

        public static SaltedHash GenerateSaltedHash(string input)
        {
            var salt = GenerateSalt();
            var hash = GenerateHash(input, salt);

            return new SaltedHash
            {
                Salt = Convert.ToBase64String(salt),
                Hash = Convert.ToBase64String(hash)
            };
        }

        public static bool IsHashValid(string input, string hash, string salt)
        {
            var hashToCompare = GenerateHash(
                input,
                Convert.FromBase64String(salt)
                );

            return Convert.ToBase64String(hashToCompare) == hash;
        }

        #region Private methods
        private static byte[] GenerateHash(string input, byte[] salt)
        {
            var hash = KeyDerivation.Pbkdf2(
                input,
                salt,
                prf: KeyDerivationPrf.HMACSHA1,
                HashingIterationsCount,
                HashSize
                );

            return hash;
        }

        private static byte[] GenerateSalt()
        {
            var salt = new byte[SaltSize];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            return salt;
        }
        #endregion
    }

    public class SaltedHash
    {
        public string Hash { get; set; } = null!;
        public string Salt { get; set; } = null!;
    }
}
