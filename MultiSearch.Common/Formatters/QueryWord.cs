using System.Collections.Generic;

namespace MultiSearch.Common.Formatters
{
    public class ResultQueryWord
    {
        public string Word { get; set; }
        public IList<ResultQueryEngine> Engines { get; set; }
    }
}