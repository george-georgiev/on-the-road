using AutoMapper;

namespace OnTheRoad.Infrastructure.Contracts
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IMapperConfigurationExpression configuration);
    }
}
