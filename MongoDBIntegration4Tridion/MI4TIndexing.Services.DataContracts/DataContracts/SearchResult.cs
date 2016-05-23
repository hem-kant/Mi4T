using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MI4TIndexing.Services.DataContracts.DataContracts
{

    [DataContract]
    public class SearchResults<T>
    {
         

        [DataMember]
        public ICollection<string> suggestions { get; set; }

        public SearchResults() { }

        [DataMember]
        public int RecordCount { get; set; }

        [DataMember]
        public IDictionary<string, ICollection<KeyValuePair<string, int>>> Facets { get; set; }

        [DataMember]
        public int CurrentPage { get; set; }

        [DataMember]
        public int MaxPages { get; set; }

    }
}
