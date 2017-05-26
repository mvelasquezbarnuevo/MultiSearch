using System;
using System.Collections.Generic;
using System.Text;

namespace MultiSearch.Common.Search
{
    public interface ISearchRequest
    {
        string Address { get; set; }
        string SearchBy { get; set; }
        string ServiceApiAddress { get; }
        List<string> Criteria { get; set; }
    }
}
