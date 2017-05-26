using MultiSearch.Common.Formatters;

namespace MultiSearch.Displays
{ 
    public interface IFlavor
    {
        void PrintMainText(string aText);
        void PrintRegularText(string aText);
        void PrintErrorText(string aText);
        void PrintNotificationText(string aText);
        void FormatAndPrintDescriptor(PluginDescriptor plugin);
    }
}
