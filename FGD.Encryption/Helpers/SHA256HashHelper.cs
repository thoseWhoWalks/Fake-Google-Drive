using System.Security.Cryptography;
using System.Text;

namespace FGD.Encryption.Helpers
{
    public static class SHA256HashHelper
    {
        public static string Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            using var algorithm = SHA256.Create();
            var hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sb = new StringBuilder();
            foreach (byte b in hash)
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
