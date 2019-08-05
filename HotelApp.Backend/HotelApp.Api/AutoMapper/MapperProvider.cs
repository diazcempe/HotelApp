using AutoMapper;
using AutoMapper.Configuration;
using SimpleInjector;

namespace HotelApp.Api.AutoMapper
{
    public class MapperProvider
    {
        private readonly Container _container;

        public MapperProvider(Container container)
        {
            _container = container;
        }

        public IMapper GetMapper()
        {
            var mce = new MapperConfigurationExpression();
            mce.ConstructServicesUsing(_container.GetInstance);

            mce.AddProfile<MappingProfile>();

            var mc = new MapperConfiguration(mce);
            mc.AssertConfigurationIsValid();

            return new Mapper(mc, t => _container.GetInstance(t));
        }
    }
}
