using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_midleware
{
    public class HeaderModel
    {
        public List<SearchResult> Results { get; set; }

        public class SearchResult
        {
            public string Id { get; set; }
            public string Image { get; set; }
            public string Title { get; set; }
        }
    }
}
