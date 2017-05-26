using MultiSearch.Common.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiSearch.Common.Formatters
{
    public class ContestResponse
    {
        public List<ISearchResponse> EngineResponses { get; set; }
        public List<string> Words { get; set; }
        public List<string> Engines { get; set; }
    }
}
