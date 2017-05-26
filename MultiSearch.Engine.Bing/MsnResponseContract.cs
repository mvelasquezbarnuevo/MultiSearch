using System.Runtime.Serialization;

namespace MultiSearch.Engine.Bing
{
    [DataContract]
    public class MsnResponseContract
    {
        [DataMember]
        public WebPages webPages { get; set; }
    }

    [DataContract]
    public class WebPages
    {
        [DataMember]
        public string webSearchUrl { get; set; }
        [DataMember]
        public long totalEstimatedMatches { get; set; }
    }
}
