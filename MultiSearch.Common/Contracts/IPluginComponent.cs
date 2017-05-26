using MultiSearch.Common.Search;
using MultiSearch.Engine.Google;
using System.Net.Http;
using System.Threading.Tasks;

namespace MultiSearch.Common.Contracts
{
    public interface IPluginComponent : ITextFormatter
    {
        Task<ISearchResponse> Search<T>(string query, T service);

        string Name { get; }
        string Address { get; }

    }
}
