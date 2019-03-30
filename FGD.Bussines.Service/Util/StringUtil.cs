using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Bussines.Service.Helper
{
    public static class StringUtil
    {
        public static String GerRandomString(this String str)
        {
            return Guid.NewGuid().ToString();
        }
    }
}
