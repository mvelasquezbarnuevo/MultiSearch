using System;
using System.Collections.Generic;

namespace MultiSearch.Api.Models
{
    public class ApiSearchEngine
    {
        public string Agent { get; set; }
        public List<ApiSearchWord> SearchWords { get; set; }
        
    }
}