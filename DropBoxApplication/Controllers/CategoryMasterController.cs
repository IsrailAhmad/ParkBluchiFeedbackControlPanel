using DropBoxApplication.App_Start;
using DropBoxApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DropBoxApplication.Controllers
{
    public class CategoryMasterController : BaseController
    {
        // GET: CategoryMaster
        public ActionResult Index()
        {
            ViewBag.LoginID = Session["LoginID"].ToString();
            ViewBag.Username = Session["Username"].ToString();
            ViewBag.Message = @TempData["message"];
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddNewCategory(FormCollection fc, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                //ViewBag.StoreId = Session["StoreId"].ToString();
                int categoryid = 0;
                string storeid = "";
                CategoryMasterModel model = new CategoryMasterModel();
                string CategoryId = fc["CategoryId"];
                string categoryname = fc["CategoryName"];
                //string menuid = fc["MenuName"];
                //if (ViewBag.StoreId == "0")
                //{
                //    storeid = fc["StoreName"];
                //}
                //else
                //{
                //    storeid = ViewBag.StoreId;
                //}

                string description = fc["CategoryDescription"];
                string url = GetUrl(2);
                url = url + "Category/AddNewCategory?CategoryId=" + CategoryId + "&CategoryName=" + categoryname + "";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await client.GetAsync(url);
                    CategoryMasterModelSingleRootObject result = new CategoryMasterModelSingleRootObject();
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var response = responseMessage.Content.ReadAsStringAsync().Result;
                        var settings = new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        };
                        result = JsonConvert.DeserializeObject<CategoryMasterModelSingleRootObject>(response);
                        if (result.data.CategoryId == 0)
                        {
                            categoryid = Convert.ToInt32(CategoryId);
                        }
                        else
                        {
                            categoryid = result.data.CategoryId;
                        }

                        if (categoryid > 0)
                        {
                            try
                            {
                                var allowedExtensions = new[]
                                {
                                 ".Jpg", ".png", ".jpg", "jpeg",".JPG",
                            };
                                //string imagepath = "http://103.233.79.234/Data/ShamSweets_Android/CategoryPictures/";
                                model.CategoryPictures = file.ToString(); //getting complete url                         
                                var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                                var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                                if (allowedExtensions.Contains(ext)) //check what type of extension  
                                {
                                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                                    string myfile = +categoryid + ext; //appending the name with id  
                                                                       // store the file inside ~/project folder(Img)  
                                                                       //var path = Path.Combine(imagepath, myfile);
                                    string path = @"C:\inetpub\wwwroot\Data\StoreFeedback_Android\CategoryPictures\" + Server.HtmlEncode(myfile);
                                    model.CategoryPictures = path;
                                    //file.SaveAs(path);

                                    var fInfo = new FileInfo(myfile);
                                    if (!fInfo.Exists)
                                    {
                                        file.SaveAs(path);
                                    }
                                    else
                                    {
                                        System.IO.File.Copy(path, path, true);
                                    }

                                }
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        else
                        {
                            TempData["message"] = "Please choose only Image file!";
                        }
                    }
                    TempData["message"] = "New Category Added Successfully!";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["message"] = "New Category Not Added Successfully!";
                return RedirectToAction("Index");
            }
        }

        //[HttpGet]
        //public async Task<ActionResult> GetAllCategoryList()
        //{
        //    List<CategoryMasterModel> olist = new List<CategoryMasterModel>();
        //   // ViewBag.StoreId = Session["StoreId"].ToString();
        //    string url = GetUrl(2);
        //    url = url + "Category/GetAllCategoryList";

        //    using (HttpClient client = new HttpClient())
        //    {

        //        HttpResponseMessage responseMessage = await client.GetAsync(url);
        //        CategoryMasterModelRootObject result = new CategoryMasterModelRootObject();
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            var response = responseMessage.Content.ReadAsStringAsync().Result;
        //            var settings = new JsonSerializerSettings
        //            {
        //                NullValueHandling = NullValueHandling.Ignore,
        //                MissingMemberHandling = MissingMemberHandling.Ignore
        //            };
        //            result = JsonConvert.DeserializeObject<CategoryMasterModelRootObject>(response);
        //            olist = result.data;
        //            ViewBag.LocalityList = olist;
        //        }
        //    }
        //    return PartialView("_Categorylist", olist);
        //}
    }
}