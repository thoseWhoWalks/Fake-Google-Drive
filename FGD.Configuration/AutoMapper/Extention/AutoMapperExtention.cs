using System;
using System.Collections.Generic;
using System.Text;

namespace FGD.Configuration 
{
    public static class MapperExtention
    {
        public static TDestination Map<TSource, TDestination>(this TDestination destination, TSource source)
        {
            return AutoMapperConfig.Mapper.Map(source, destination);
        }
    }
}
