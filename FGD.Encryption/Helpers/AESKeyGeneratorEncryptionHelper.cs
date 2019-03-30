using System;

namespace FGD.Encryption.Helpers
{
    public class AESKeyGeneratorEncryptionHelper
    {
        public static string GetAESKey()
        { 
            return Guid.NewGuid().ToString();
        }
    }
}
