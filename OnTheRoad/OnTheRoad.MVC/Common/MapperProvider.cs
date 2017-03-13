using AutoMapper;
using OnTheRoad.Infrastructure.Mapping;

namespace OnTheRoad.MVC.Common
{
    public static class MapperProvider
    {
        static MapperProvider()
        {
            MapperProvider.Mapper = AutoMapperConfig.Configuration?.CreateMapper();
        }

        public static IMapper Mapper { get; set; }
    }
}