using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace FGD.Encryption.Helpers
{
    public class SymetricTokenEncryptionHelper
    {
        public static SymmetricSecurityKey GetSymmetricSecurityKey(String key)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }

    }
}
