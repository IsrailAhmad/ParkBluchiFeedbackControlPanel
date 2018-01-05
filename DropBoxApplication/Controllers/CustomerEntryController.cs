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
    public class CustomerEntryController : BaseController
    {
        // GET: CustomerEntry
        public ActionResult Index()
        {
            ViewBag.Message = @TempData["message"];
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SaveCustomer(CustomerEntryModel model)
        {
            CustomerEntryModelObjectRootModel obj = new CustomerEntryModelObjectRootModel();
            string url = GetUrl(2);
            url = url + "WebSiteLogin/SaveCustomerData?Name=" + model.CustomerName + "&PhoneNo=" + model.PhoneNumber + "&EmailId=" + model.Email + "&Address=" + model.Address + "";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = responseMessage.Content.ReadAsStringAsync().Result;
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    obj = JsonConvert.DeserializeObject<CustomerEntryModelObjectRootModel>(response, settings);
                    if (obj.data.isSuccess)
                    {
                        ViewBag.Message = "Save Data Successfully!";
                    }
                    else
                    {
                        ViewBag.Message = "Data Not Saved!";
                    }

                }
            }
            TempData["message"] = ViewBag.Message;
            return RedirectToAction("Index");
        }
    }
}