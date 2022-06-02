
using Middleware_Api_Lib;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace DEPT_Api.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public async Task<Trailer> Get(string search)
        {
            return await LoadTrailer(search);
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        private async Task<Trailer> LoadTrailer(string s)
        {
            MiddlewareApiHelper api = new MiddlewareApiHelper();
            return await api.GetTrailer(s);
        }
    }

}
