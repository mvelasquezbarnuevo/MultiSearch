using System;
using System.Threading.Tasks;
using MultiSearch.Common.Contracts;
using MultiSearch.Common.Search;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.ComponentModel.Composition;
using System.Collections.Generic;
using System.Drawing;
using MultiSearch.Common.Formatters;

namespace MultiSearch.Engine.Bing
{
    [Export(typeof(IPluginComponent))]
    public class MsnEngine : IPluginComponent
    {
        public string Name => "Msn search";
        public string Address => Properties.Resources.URL;
        private string Key => Properties.Resources.KEY;
        private HttpClient _client;
        private string Header
        {
            get
            {
                return Properties.Resources.HEADER;
            }
        }
        private string GetApiQuery(string query) => $"{Address}{query}";

        public async Task<ISearchResponse> Search<T>(string query, T service)
        {
            //_client = service as HttpClient;
            using (var client = new HttpClient())
            using (var response = await client.SendAsync(GetRequestMessage(GetApiQuery(query))))
            {
                var streamContent = await response.Content.ReadAsStreamAsync();
                var jsonSerializer = new DataContractJsonSerializer(typeof(MsnResponseContract));
                var crudData = (MsnResponseContract)jsonSerializer.ReadObject(streamContent);

                return new SearchResponse
                {
                    Word = query,
                    EngineName = Name,
                    RecordsCount = crudData?.webPages?.totalEstimatedMatches
                };
            }

        }

        private HttpRequestMessage GetRequestMessage(string apiQuery)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(apiQuery),
                Method = HttpMethod.Get,
            };
            request.Headers.Add(Header, Key);

            return request;
        }

        #region ITextFormatter implementation
        public string PrintableName => "    * {0}{1}{2} Search";

        public PluginFormatter[] NameFormatter
        {
            get
            {
                var formatter = new List<PluginFormatter>();
                formatter.Add(new PluginFormatter { Word = "M", Color = Color.Blue });
                formatter.Add(new PluginFormatter { Word = "S", Color = Color.Blue });
                formatter.Add(new PluginFormatter { Word = "N", Color = Color.Blue });
                return formatter.ToArray();
            }
        }
        #endregion
    }
}
