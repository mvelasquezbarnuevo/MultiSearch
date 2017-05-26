using MultiSearch.Common.Formatters;

namespace MultiSearch.Engine.Google
{
    public interface ITextFormatter
    {
        string PrintableName { get; }

        PluginFormatter[] NameFormatter { get; }
    }
}