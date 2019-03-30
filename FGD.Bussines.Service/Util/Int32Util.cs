using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Bussines.Service
{
    public static class Int32Util
    {
        public static int CLASTER_SIZE = 1024;

        public static Int32 KilobytesToGigabytes(this Int32 kilobytes)
        {
            return Convert.ToInt32(
                Convert.ToInt32(kilobytes / CLASTER_SIZE ) / CLASTER_SIZE
                );
        }
    }
}
