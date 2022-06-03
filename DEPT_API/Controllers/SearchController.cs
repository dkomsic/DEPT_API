using DEPT_API.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DEPT_Api.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index(string search)
        {
            TrailerModel res = new TrailerModel();
            if (search != null)
            {
                string url = "https://localhost:44326/api/Values?search=";
                var api = WebApiApplication.httpClient.GetAsync(url + search);
                api.Wait();
                {
                    if (api.Result.IsSuccessStatusCode)
                    {
                        var res1 = api.Result.Content.ReadAsAsync<TrailerModel>();
                        res1.Wait();
                        res = res1.Result;
                    }
                    else
                    {
                        throw new Exception("");
                    }
                }
            }
            return View(res);
        }

        public async Task<TrailerModel> GetTrailer(string search)
        {
            string url = "https://localhost:44326/api/Values?search=";
            using (HttpResponseMessage response = await WebApiApplication.httpClient.GetAsync(url + search))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsAsync<TrailerModel>();
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}