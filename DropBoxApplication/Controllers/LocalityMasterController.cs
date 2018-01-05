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
    public class LocalityMasterController : BaseController
    {
        // GET: LocalityMaster
        public ActionResult Index()
        {
            ViewBag.Message = @TempData["message"];
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddNewLocalityName(LocalityMasterModel model)
        {
            string url = GetUrl(2);
            url = url + "/Localities/AddNewLocalityName?strLocalityName=" + model.LocalityName + "&strImageUrl=" + model.ImageUrl + "";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url);
                Response data = new Response();
                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = responseMessage.Content.ReadAsStringAsync().Result;
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    data = JsonConvert.DeserializeObject<Response>(response, settings);
                    //result = obj.data;
                    if (data.isSuccess)
                    {
                        TempData["message"] = "New Category Added Successfully!";

                    }
                    else
                    {
                        TempData["message"] = "New Category Not Added Successfully!";
                        //return RedirectToAction("Index");
                    }
                    //ViewBag.data = data;
                }
                TempData["message"] = "New Category Not Added Successfully!";
            }
            return RedirectToAction("Index");
        }
    }
}