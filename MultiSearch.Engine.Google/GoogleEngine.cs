using MultiSearch.Common.Contracts;
using MultiSearch.Common.Search;
using System.Net.Http;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Drawing;
using MultiSearch.Common.Formatters;
using System;

namespace MultiSearch.Engine.Google
{

    [Export(typeof(IPluginComponent))]
    public class GoogleEngine : IPluginComponent, IDisposable
    {
        public string Address => Properties.Resources.URL;
        public string Name => "Google";
        private string GetApiQuery(string query) => $"{Address}{query}";

        private HttpClient _client;

        public async Task<ISearchResponse> Search<T>(string query, T service)
        {
            //_client = service as HttpClient;
            using (var client = new HttpClient())
            using (var response = await client.GetStreamAsync(GetApiQuery(query)))
            {
                var jsonSerializer = new DataContractJsonSerializer(typeof(GoogleResponseContract));
                var crudData = (GoogleResponseContract)jsonSerializer.ReadObject(response);

                return new SearchResponse
                {
                    Word = query,
                    EngineName = Name,
                    RecordsCount = crudData?.queries?.request?[0]?.totalResults
                };

            }

        }

        public void Dispose()
        {
            _client = null;
        }


        #region ITextFormatter implementation
        public string PrintableName => "    * {0}{1}{2}{3}{4}{5}";
        public PluginFormatter[] NameFormatter
        {
            get
            {
                var formatter = new List<PluginFormatter>();
                formatter.Add(new PluginFormatter { Word = "G", Color = Color.Blue });
                formatter.Add(new PluginFormatter { Word = "O", Color = Color.Red });
                formatter.Add(new PluginFormatter { Word = "O", Color = Color.Yellow });
                formatter.Add(new PluginFormatter { Word = "G", Color = Color.Blue });
                formatter.Add(new PluginFormatter { Word = "L", Color = Color.Green });
                formatter.Add(new PluginFormatter { Word = "E", Color = Color.Red });
                return formatter.ToArray();
            }
        }

        #endregion

    }


}
