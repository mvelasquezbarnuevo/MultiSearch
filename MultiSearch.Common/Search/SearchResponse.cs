using System;

namespace MultiSearch.Common.Search
{
    public class SearchResponse : ISearchResponse
    {
        public string Word { get; set; }
        public string EngineName { get; set; }
        public long? RecordsCount { get; set; }
        public bool Success
        {
            get => RecordsCount > 0;
        }

    }
}
