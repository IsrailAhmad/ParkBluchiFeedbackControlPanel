using DropBoxApplication.App_Start;
using DropBoxApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace DropBoxApplication.Controllers
{
    public class EverGreenDailyActivityController : BaseController
    {
        // GET: EverGreenDailyActivity
        public ActionResult Index()
        {
            ViewBag.LoginID = Session["LoginID"].ToString();
            ViewBag.Username = Session["Username"].ToString();
            ViewBag.Message = "Your application Daily Activity page.";
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetEverGreenOrders(int OrderStatusId)
        {
            List<CustomerOrderModel> olist = new List<CustomerOrderModel>();
            if (ModelState.IsValid)
            {
                ViewBag.LoginID = Session["LoginID"].ToString();
                ViewBag.Username = Session["Username"].ToString();
                ViewBag.Message = "Your application Daily Activity page.";
                string url = GetUrl(2);
                url = url + "UserLogin/MyAllOrderListByStatusId?StatusId=" + OrderStatusId;
                //CustomerOrderModel order = new CustomerOrderModel();
                OrderRootObject obj = new OrderRootObject();

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage CustomerresponseMessage = await client.GetAsync(url);
                    if (CustomerresponseMessage.IsSuccessStatusCode)
                    {
                        var response = CustomerresponseMessage.Content.ReadAsStringAsync().Result;
                        var settings = new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        };
                        obj = JsonConvert.DeserializeObject<OrderRootObject>(response, settings);
                        olist = obj.data;
                        ViewBag.TransactionList = olist;
                    }
                }
                return View(olist);
            }
            return View(olist);

        }

        [HttpGet]
        public async Task<ViewResult> OrderDetails(int OrderId)
        {
            CustomerOrderDetailsRootObject orderDetails = new CustomerOrderDetailsRootObject();
            CustomerOrderModel custModel = new CustomerOrderModel();
            if (ModelState.IsValid)
            {
                ViewBag.LoginID = Session["LoginID"].ToString();
                ViewBag.Username = Session["Username"].ToString();
                ViewBag.OrderId = OrderId;
                string strurl = GetUrl(2);
                strurl = strurl + "UserLogin/GetOrderByOrderId?orderid=" + OrderId;
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
                        orderDetails = JsonConvert.DeserializeObject<CustomerOrderDetailsRootObject>(response, settings);
                        custModel = orderDetails.data;
                        ViewBag.Transaction = custModel;
                    }
                }              
                return View(custModel);
            }
            return View(custModel);
        }

    }
}