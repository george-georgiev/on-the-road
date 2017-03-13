using AutoMapper;
using OnTheRoad.Infrastructure.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OnTheRoad.Infrastructure.Mapping
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration Configuration { get; private set; }

        public void Execute(Assembly assembly)
        {
            Configuration = new MapperConfiguration(
                cfg =>
                {
                    var types = assembly.GetExportedTypes();
                    LoadBothWaysMapping(types, cfg);
                    LoadStandardMappings(types, cfg);
                    LoadReverseMappings(types, cfg);
                    LoadCustomMappings(types, cfg);
                });
        }

        private void LoadBothWaysMapping(IEnumerable<Type> types, IMapperConfigurationExpression mapperConfigurationExpression)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapBothWays<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        }).ToArray();

            foreach (var map in maps)
            {
                mapperConfigurationExpression.CreateMap(map.Source, map.Destination);
                mapperConfigurationExpression.CreateMap(map.Destination, map.Source);
            }
        }

        private void LoadStandardMappings(IEnumerable<Type> types, IMapperConfigurationExpression mapperConfigurationExpression)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Source = i.GetGenericArguments()[0],
                            Destination = t
                        }).ToArray();

            foreach (var map in maps)
            {
                mapperConfigurationExpression.CreateMap(map.Source, map.Destination);
            }
        }

        private void LoadReverseMappings(IEnumerable<Type> types, IMapperConfigurationExpression mapperConfigurationExpression)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapTo<>) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select new
                        {
                            Destination = i.GetGenericArguments()[0],
                            Source = t
                        }).ToArray();

            foreach (var map in maps)
            {
                mapperConfigurationExpression.CreateMap(map.Source, map.Destination);
            }
        }

        private void LoadCustomMappings(IEnumerable<Type> types, IMapperConfigurationExpression mapperConfigurationExpression)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where typeof(IHaveCustomMappings).IsAssignableFrom(t) &&
                              !t.IsAbstract &&
                              !t.IsInterface
                        select (IHaveCustomMappings)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(mapperConfigurationExpression);
            }
        }
    }
}
