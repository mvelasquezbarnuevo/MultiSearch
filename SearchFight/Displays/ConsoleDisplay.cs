using System;
using MultiSearch.SearchingCore.Engines;
using log4net;
using MultiSearch.Common.Formatters;
using System.Collections.Generic;
using SearchFight;

namespace MultiSearch.Displays
{
    public class ConsoleDisplay : IDisplayComparer
    {
        private readonly ILog _logger;
        private readonly IFlavor _flavor;
        public ConsoleDisplay(IFlavor flavor, ILog logger)
        {
            _logger = logger;
            _flavor = flavor;
        }
        public void ShowError(string message)
        {
            _flavor.PrintErrorText(message);
            Console.ReadLine();
        }
        public void ShowInfo(string message)
        {
            _flavor.PrintNotificationText(message);
            Console.WriteLine();
        }
        public void PrintWords(IList<ResultQueryWord> words)
        {
            Console.WriteLine();
            foreach (var word in words)
            {
                string singleLine = $"    {word.Word}: ";
                foreach (var engine in word.Engines)
                {
                    singleLine += $"{engine.Name}: {engine.Total.ToString()} ";
                }
                Console.WriteLine(singleLine);
            }
        }
        public void PrintEngines(IList<ResultQueryEngine> winners)
        {
            Console.WriteLine();
            foreach (var engine in winners)
            {
                Console.WriteLine($"    { engine.Name} winner: {engine.Word}");
            }
        }
        public void PrintWinner(string winner)
        {
            Console.WriteLine();
            _flavor.PrintNotificationText($"    Total winner: {winner}");
        }
        public void ShowResults(IEngineLoader engineLoader, ConsoleResults results)
        {

            ShowInfo("     -->  CONTEST RESULT  <--");
            try
            {
                PrintWords(results.Words);
                PrintEngines(results.Winners);
                PrintWinner(results.WinnerWord);
            }
            catch (Exception ex)
            {
                _logger.Error("ConsoleDisplay: There's an reading plugin formatters.", ex);
                throw ex;
            }

            Console.ReadLine();


        }
        public void ShowAvailableEngines(IEngineLoader engineLoader)
        {
            ShowInfo("     -->  LIST OF SEARCH ENGINES  <---");
            try
            {
                foreach (var plugin in engineLoader.GetAvailablePlugins())
                    _flavor.FormatAndPrintDescriptor(plugin);

            }
            catch (Exception ex)
            {
                _logger.Error("ConsoleDisplay: There's an error reading plugin formatters.", ex);
                throw ex;
            }

            Console.WriteLine(Environment.NewLine);

        }
        public void SetTitle()
        {
            _flavor.PrintMainText("SEARCH FIGHT");

        }

      
    }


}
