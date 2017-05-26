using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MultiSearch.Common.Search
{
    public interface IEngineBase
    {
        string Address { get; set; }
        Task<ISearchResponse> Search(ISearchRequest request);
        Task<ISearchResponse> SendRequest();
    }
}
