using log4net;
using MultiSearch.Common.Contracts;
using MultiSearch.Common.Formatters;
using MultiSearch.Common.Search;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MultiSearch.SearchingCore.Engines
{
    public class EngineLoader : IEngineLoader
    {
        [ImportMany(typeof(IPluginComponent))]
        private IEnumerable<Lazy<IPluginComponent>> _plugins;
        private readonly ILog _logger;

        public EngineLoader(ILog logger)
        {
            _logger = logger;
        }

        public void Load(DirectoryCatalog directory)
        {
            try
            {

                var catalog = new AggregateCatalog();
                catalog.Catalogs.Add(directory);
                CompositionContainer container = new CompositionContainer(catalog);
                container.ComposeParts(this);
            }
            catch (Exception ex)
            {
                _logger.Error("SearchingCore: Search plugins couldn't be loaded.", ex);
                throw ex;
            }

        }

        public List<PluginDescriptor> GetAvailablePlugins()
        {
            var result = new List<PluginDescriptor>();
            foreach (Lazy<IPluginComponent> com in _plugins)
            {
                var descriptor = new PluginDescriptor
                {
                    Name = com.Value.Name,
                    NameFormatter = com.Value.NameFormatter,
                    PrintableName = com.Value.PrintableName
                };

                result.Add(descriptor);
            }
            return result;
        }

        public async Task<ContestResponse> SendRequest(ISearchRequest searchrequest, List<IPluginComponent> services)
        {
            try
            {
                var collectedResponses = new List<Task<ISearchResponse>>();

                foreach (var com in services)
                    foreach (var query in searchrequest.Criteria)
                    {
                        collectedResponses.Add(com.Search(query, new HttpClient()));
                    }
                return FormatRespose(searchrequest, await Task.WhenAll(collectedResponses));
            }
            catch (Exception ex)
            {
                _logger.Error("SearchingCore: There's an error retriving plugins responses.", ex);
                throw ex;
            }
        }

        public async Task<ContestResponse> SendRequest(ISearchRequest searchrequest)
        {
            try
            {
                var collectedResponses = new List<Task<ISearchResponse>>();

                foreach (var com in _plugins)
                    foreach (var query in searchrequest.Criteria)
                    {
                        collectedResponses.Add(com.Value.Search(query, new HttpClient()));
                    }

                return FormatRespose(searchrequest, await Task.WhenAll(collectedResponses));
            }
            catch (Exception ex)
            {
                _logger.Error("SearchingCore: There's an error collecting plugins responses.", ex);
                throw ex;
            }

        }

        public ContestResponse FormatRespose(ISearchRequest searchrequest, ISearchResponse[] searchResults)
        {
            if (searchrequest != null && searchResults != null)
            {
                var engines = from e in searchResults.AsEnumerable().GroupBy(p => p.EngineName).Select(s => s.First()).ToList()
                              select e.EngineName;

                return new ContestResponse
                {
                    EngineResponses = searchResults.ToList(),
                    Words = searchrequest.Criteria,
                    Engines = engines.ToList()
                };
            }

            return new ContestResponse();
        }

        public bool Ready()
        {
            return _plugins.Any();
        }

    }


}
