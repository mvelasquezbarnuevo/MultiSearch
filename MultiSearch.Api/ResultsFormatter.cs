using MultiSearch.Api.Models;
using MultiSearch.Common.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiSearch.Api
{
    public class ResultsFormatter : BaseResultFormatter
    {
        public List<ApiSearchEngine> SearchEngines { get; set; }
        public override void Fill(ContestResponse args)
        {
            base.Fill(args);
            SearchEngines = new List<ApiSearchEngine>();

            foreach (var engine in args.Engines)
            {
                var searchEngine = new ApiSearchEngine
                {
                    Agent = engine,
                    SearchWords = new List<ApiSearchWord>()
                };

                var winnerByAgent = args.EngineResponses.Where(r => r.EngineName == engine).OrderByDescending(e => e.RecordsCount).FirstOrDefault();

                foreach (var searchWordsEngine in args.EngineResponses.FindAll(r => r.EngineName == engine))
                {
                    var searchWord = new ApiSearchWord
                    {
                        Word = searchWordsEngine.Word,
                        Count = searchWordsEngine.RecordsCount,
                        IsWinner = (engine == winnerByAgent.EngineName && searchWordsEngine.RecordsCount == winnerByAgent.RecordsCount)
                    };
                    searchEngine.SearchWords.Add(searchWord);
                }
                SearchEngines.Add(searchEngine);


            }
        }
    }
}