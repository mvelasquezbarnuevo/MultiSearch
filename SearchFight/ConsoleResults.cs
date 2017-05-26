using MultiSearch.Common.Formatters;
using System.Collections.Generic;
using System.Linq;

namespace SearchFight
{
    public class ConsoleResults : BaseResultFormatter
    {
        public IList<ResultQueryWord> Words { get; set; }
        public IList<ResultQueryEngine> Winners { get; set; }

        public override void Fill(ContestResponse args)
        {
            base.Fill(args);
            Words = new List<ResultQueryWord>();
            Winners = new List<ResultQueryEngine>();

            foreach (var word in args.Words)
            {
                var resultWord = new ResultQueryWord
                {
                    Word = word,
                    Engines = new List<ResultQueryEngine>()
                };

                foreach (var singleEngine in args.EngineResponses.FindAll(w => w.Word == word))
                    resultWord.Engines.Add(new ResultQueryEngine
                    {
                        Name = singleEngine.EngineName,
                        Total = singleEngine.RecordsCount
                    });


                Words.Add(resultWord);
            }

            foreach (var engine in args.Engines)
            {
                var winnerByEngine = args.EngineResponses.Where(r => r.EngineName == engine)
                                                    .OrderByDescending(e => e.RecordsCount).FirstOrDefault();
                Winners.Add(new ResultQueryEngine
                {
                    Name = winnerByEngine.EngineName,
                    Word = winnerByEngine.Word
                });
            }
        }
    }

}
