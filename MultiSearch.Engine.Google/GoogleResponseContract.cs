using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MultiSearch.Engine.Google
{
    [DataContract]
    public class GoogleResponseContract
    {
        [DataMember]
        public Queries queries { get; set; }
    }

    [DataContract]
    public class Queries
    {
        [DataMember]
        public Request[] request { get; set; }
    }


    [DataContract]
    public class Request
    {
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public long totalResults { get; set; }
    }
}
