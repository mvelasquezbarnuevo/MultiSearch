
using SearchFight.Configuration;
using SimpleInjector;
using System.Linq;

namespace SearchFight
{
    class Program
    {
        static readonly Container _container = new Container();
        static Program()
        {
            SimpleInjectorLoader.Load(_container);
        }
        static void Main(string[] args)
        {
            var args1 = new string[] { ".net", "java" };//

            var handler = _container.GetInstance<ConsoleHandler>();
            handler.Start(args1.ToList());
        }
    }
}
