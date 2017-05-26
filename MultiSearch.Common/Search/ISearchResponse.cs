using System;
using System.Collections.Generic;
using System.Text;

namespace MultiSearch.Common.Search
{
    public interface ISearchResponse
    {
        string Word { get; set; }
        string EngineName { get; set; }
        long? RecordsCount { get; set; }
        bool Success { get; }
    }
}
