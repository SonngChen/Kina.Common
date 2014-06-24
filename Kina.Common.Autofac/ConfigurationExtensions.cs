using Autofac;
using Kina.Common.Components;
using Kina.Common.Configurations;

namespace Kina.Common.Autofac
{
    public static class ConfigurationExtensions
    {
        public static Configuration UseAutofac(this Configuration configuration)
        {
            return UseAutofac(configuration, new ContainerBuilder());
        }

        public static Configuration UseAutofac(this Configuration configuration, ContainerBuilder containerBuilder)
        {
            ComponentContainer.SetContainer(new AutofacObjectContainer(containerBuilder));
            return configuration;
        }
    }
}
