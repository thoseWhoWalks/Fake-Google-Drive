using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Bussines.Service
{
    public static class Int64Util
    {
        public static int CLASTER_SIZE = 1024;

        public static int BytesToKilobytes(this long bytes)
        {
            return Convert.ToInt32(bytes / CLASTER_SIZE);
        }
    }
}
