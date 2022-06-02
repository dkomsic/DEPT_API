using System.Net.Http;
using System.Web.Mvc;

namespace DEPT_Api.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            string url = "https://localhost:44326/api/Values?search=";
            //todo create new static client for DEPT api
            HttpClient client = new HttpClient();
            return View();
        }
    }
}