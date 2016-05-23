using System.Collections.Generic;
using System.Collections.Specialized;


namespace MI4TSearching.SearchService.BAL.Model
{
    public class SearchRequest
    {
        public SearchRequest() { }

        public HybridDictionary Filters { get; set; }

        public List<string> Facets { get; set; }

        public string ContentType { get; set; }

        public bool SuggestionRequest { get; set; }

        public string SuggestionQuery { get; set; }        

        public string QueryType { get; set; }

        public int CurrentPage { get; set; }

        public int RecordSize { get; set; }

        public string SortField { get; set; }

        public int SortOrder { get; set; }

        public string PublicationID { get; set; }
    }
}
