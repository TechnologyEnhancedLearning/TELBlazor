using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TELBlazor.Components.OptionalImplementations.Test.TestComponents.SearchExperiment
{
    //qqqq no mvp
    public class SearchRequest
    {
        public string SearchTerm { get; set; } = string.Empty;
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public Dictionary<string, object> AdditionalParameters { get; set; } = new();
    }

    public class SearchResult
    {
        public IEnumerable<string> Results { get; set; } = Enumerable.Empty<string>();
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public bool HasMore => (PageIndex + 1) * PageSize < TotalCount;
    }

    public class SuggestionResult
    {
        public IEnumerable<string> Suggestions { get; set; } = Enumerable.Empty<string>();
        public int Count { get; set; }
    }
}
