using DropBoxApplication.App_Start;
using DropBoxApplication.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace DropBoxApplication.Controllers
{
    public class OperationController : BaseController
    {
        // GET: Operation
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> AcceptOrder(int? OrderId)
        {
            UserRootObject lRole = new UserRootObject();
            Response result = new Response();
            ViewBag.LoginID = Session["LoginID"].ToString();
            ViewBag.Username = Session["Username"].ToString();
            ViewBag.OrderId = OrderId;
            string strurl = GetUrl(2);
            strurl = strurl + "UserLogin/AcceptOrder?orderid=" + OrderId;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage CustomerresponseMessage = await client.GetAsync(strurl);
                if (CustomerresponseMessage.IsSuccessStatusCode)
                {
                    var response = CustomerresponseMessage.Content.ReadAsStringAsync().Result;
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    lRole = JsonConvert.DeserializeObject<UserRootObject>(response, settings);
                    result = lRole.response;                   
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> DeclineOrder(int? OrderId)
        {
            UserRootObject lRole = new UserRootObject();
            Response result = new Response();
            ViewBag.LoginID = Session["LoginID"].ToString();
            ViewBag.Username = Session["Username"].ToString();
            ViewBag.OrderId = OrderId;
            string strurl = GetUrl(2);
            strurl = strurl + "UserLogin/DeclineOrder?orderid=" + OrderId;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage CustomerresponseMessage = await client.GetAsync(strurl);
                if (CustomerresponseMessage.IsSuccessStatusCode)
                {
                    var response = CustomerresponseMessage.Content.ReadAsStringAsync().Result;
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    lRole = JsonConvert.DeserializeObject<UserRootObject>(response, settings);
                    result = lRole.response;
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}