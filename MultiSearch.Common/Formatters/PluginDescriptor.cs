using System.Collections.Generic;
using System.Drawing;

namespace MultiSearch.Common.Formatters
{
    public class PluginDescriptor
    {
        public string Name { get; set; }
        public PluginFormatter[] NameFormatter { get; set; }
        public string PrintableName { get; set; }
    }


    public class PluginFormatter
    {
        public string Word { get; set; }
        public Color Color { get; set; }
    }
}
