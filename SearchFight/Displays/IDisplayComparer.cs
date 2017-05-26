using MultiSearch.Common.Formatters;
using MultiSearch.SearchingCore.Engines;
using SearchFight;

namespace MultiSearch.Displays
{
    public interface IDisplayComparer
    {

        void SetTitle();
        void ShowError(string message);
        void ShowInfo(string message);
        void ShowResults(IEngineLoader _engineLoader, ConsoleResults results);
        void ShowAvailableEngines(IEngineLoader loader);
    }
}