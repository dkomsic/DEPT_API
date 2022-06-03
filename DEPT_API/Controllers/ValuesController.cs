
using DEPT_API.Helper;
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
            if (CacheData.Get(search) != null)
                return (Trailer)CacheData.Get(search);
            else
            {
                var res = await LoadTrailer(search);
                CacheData.Add(search, res);
                return res;
            }
        }

        private async Task<Trailer> LoadTrailer(string s)
        {
            if (s == null) return new Trailer();
            MiddlewareApiHelper api = new MiddlewareApiHelper();
            return await api.GetTrailer(s);
        }
    }

}
