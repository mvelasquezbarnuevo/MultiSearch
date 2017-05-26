using log4net;
using MultiSearch.Displays;
using MultiSearch.SearchingCore.Engines;
using SimpleInjector;
using System.Linq;

namespace SearchFight.Configuration
{
    public static class SimpleInjectorLoader
    {

        public static void Load(Container container)
        {

            var repositoryAssembly = typeof(EngineLoader).Assembly;

            var registrations =
                from type in repositoryAssembly.GetExportedTypes()
                where type.Namespace.Contains("MultiSearch") || type.Namespace.Contains("SearchFight")
                where type.GetInterfaces().Any()
                select new { Service = type.GetInterfaces().Single(), Implementation = type };

            foreach (var reg in registrations)
            {
                container.Register(reg.Service, reg.Implementation, Lifestyle.Transient);
            }


            container.RegisterSingleton(LogManager.GetLogger(typeof(object)));
            container.Register<ConsoleHandler>();
            container.Register<IDisplayComparer, ConsoleDisplay>();
            container.Register<IFlavor, ColorfulFlavor>();

            container.Verify();
        }
    }
}
