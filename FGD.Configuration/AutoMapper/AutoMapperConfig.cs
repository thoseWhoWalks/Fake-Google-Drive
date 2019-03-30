using AutoMapper;
using FGD.Configuration.AutoMapper;

namespace FGD.Configuration
{
    public static class AutoMapperConfig { 

        public static IMapper Mapper;

        public static void Configure()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BussinesToApiAutoMapperProfile>();
                cfg.AddProfile<DataToBussinesAutoMapperProfile>();
            });

            Mapper = config.CreateMapper();
        }
    }
}
