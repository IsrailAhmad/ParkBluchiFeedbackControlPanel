using DropBoxApplication.Models;
using DropBoxApplication.App_Start;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DropBoxApplication.Controllers
{
    public class MainController : BaseController
    {
        // GET: Main
        public ActionResult Dashboard()
        {
            ViewBag.LoginID = Session["LoginID"].ToString();
            ViewBag.Username = Session["Username"].ToString();
            ViewBag.Message = "Your application Dashboard page.";
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetOrderStatusCount()
        {
            string url = GetUrl(2);
            url = url + "/UserLogin/GetAllOrders";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url);
                //List<OrderStatusModel> result = new List<OrderStatusModel>();
                OrderStatusRootObject obj = new OrderStatusRootObject();
                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = responseMessage.Content.ReadAsStringAsync().Result;
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    obj = JsonConvert.DeserializeObject<OrderStatusRootObject>(response, settings);
                    //result = obj.data;
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
    }
}