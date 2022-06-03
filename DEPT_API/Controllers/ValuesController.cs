
using Middleware_Api_Lib;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace DEPT_Api.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values/avatar
        public async Task<Trailer> Get(string search)
        {
            return await LoadTrailer(search);
        }

        private async Task<Trailer> LoadTrailer(string s)
        {
            if (s == null) return new Trailer();
            MiddlewareApiHelper api = new MiddlewareApiHelper();
            return await api.GetTrailer(s);
        }
    }

}
