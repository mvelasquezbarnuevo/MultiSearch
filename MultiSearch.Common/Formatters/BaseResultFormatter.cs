using System.Linq;

namespace MultiSearch.Common.Formatters
{
    public abstract class BaseResultFormatter 
    {
        public string WinnerWord { get; set; }

        public virtual void Fill(ContestResponse args)
        {
            var winner = args.EngineResponses.OrderByDescending(r => r.RecordsCount).FirstOrDefault();
            if (winner != null)
                WinnerWord = winner.Word;
            
        }
    }
}
