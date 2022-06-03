using DEPT_API.Models;
using System;
using System.Net.Http;
using System.Web.Configuration;
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
                res = GetTrailer(search);
            }
            return View(res);
        }

        public TrailerModel GetTrailer(string search)
        {
            var masterApi = WebApiApplication.httpClient.GetAsync(WebConfigurationManager.AppSettings["apiUrl"] + search);
            masterApi.Wait();
            {
                if (masterApi.Result.IsSuccessStatusCode)
                {
                    var res = masterApi.Result.Content.ReadAsAsync<TrailerModel>();
                    res.Wait();
                    return res.Result;
                }
                else
                {
                    throw new Exception(masterApi.Result.ReasonPhrase);
                }
            }
        }
    }
}