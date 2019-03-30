using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Bussines.Model
{
    public class AuthJWTModel
    {
        public String Issuer { get; set; }

        public String Audience { get; set; }

        public String Key { get; set; }

        public int Lifetime { get; set; }

    }
}
