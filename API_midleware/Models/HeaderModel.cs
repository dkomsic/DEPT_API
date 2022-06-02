using System.Collections.Generic;

namespace Middleware_Api_Lib
{
    public class HeaderModel
    {
        public List<SearchResult> Results { get; set; }

        public class SearchResult
        {
            public string Id { get; set; }
            public string Image { get; set; }
        }
    }
}
