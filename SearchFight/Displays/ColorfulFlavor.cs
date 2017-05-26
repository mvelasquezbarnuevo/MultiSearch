using System.Collections.Generic;
using Console = Colorful.Console;
using Colorful;
using System.Drawing;
using MultiSearch.Common.Formatters;
using System.Linq;

namespace MultiSearch.Displays
{
    public class ColorfulFlavor : IFlavor
    {
        public void PrintMainText(string aText)
        {
            int DA = 244;
            int V = 212;
            int ID = 255;

            Console.WriteAscii(aText, Color.FromArgb(DA, V, ID));
        }

        public void PrintRegularText(string aText)
        {
        }

        public void PrintErrorText(string aText)
        {
            Console.WriteLine(aText, Color.Red);
        }

        public void PrintNotificationText(string aText)
        {
            Console.WriteLine(aText, Color.Yellow);
        }

        public void FormatAndPrintDescriptor(PluginDescriptor plugin)
        {
            List<Formatter> formatter = new List<Formatter>();

            for (int i = 0; i < plugin.NameFormatter.Length; i++)
                formatter.Add(new Formatter(plugin.NameFormatter[i].Word, plugin.NameFormatter[i].Color));

            if (formatter.Any())
                Console.WriteLineFormatted(plugin.PrintableName, Color.Gray, formatter.ToArray());
        }


    }

}
