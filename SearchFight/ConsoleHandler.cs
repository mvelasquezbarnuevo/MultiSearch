using log4net;
using MultiSearch.Common.Handlers;
using MultiSearch.Common.Search;
using MultiSearch.Displays;
using MultiSearch.SearchingCore.Engines;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SearchFight
{
    public class ConsoleHandler : IValidation
    {
        private readonly IEngineLoader _engineLoader;
        private readonly IDisplayComparer _displayResults;
        private readonly ILog _logger;


        public ConsoleHandler(IEngineLoader engineLoader,
                            IDisplayComparer displayResults,
                            ILog logger)
        {
            _engineLoader = engineLoader;
            _displayResults = displayResults;
            _logger = logger;
        }


        public void Start(List<string> args)
        {

            _displayResults.SetTitle();

            if (!IsValid(args))
            {
                _displayResults.ShowError("The input data is not valid!");
                return;
            }

            _engineLoader.Load(new DirectoryCatalog(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));

            if (!_engineLoader.Ready())
            {
                _displayResults.ShowError("No engines were found!. Please compile engines projects; xcopy command will move the dll into bin folder.");
                return;
            }

            _displayResults.ShowAvailableEngines(_engineLoader);

            _displayResults.ShowInfo("Please wait, while engines are working...");

            var result = _engineLoader.SendRequest(new SearchRequest { Criteria = args }).Result;

            var resultFormatter = new ConsoleResults();
            resultFormatter.Fill(result);

            _displayResults.ShowResults(_engineLoader, resultFormatter);

        }




        public bool IsValid<T>(T obj)
        {
            if (obj == null)
                return false;

            var validItems = ((List<string>)(object)obj).Select(e => e.Trim() != "");
            if (!(validItems.Count() > 1))
                return false;

            return true;
        }


    }
}
