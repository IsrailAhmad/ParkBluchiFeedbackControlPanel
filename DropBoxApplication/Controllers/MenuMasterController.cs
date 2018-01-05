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
    public class MenuMasterController : BaseController
    {
        // GET: MenuMaster

        public async Task<ActionResult> Index()
        {
            ViewBag.LoginID = Session["LoginID"].ToString();
            ViewBag.Username = Session["Username"].ToString();
            ViewBag.Message = @TempData["message"];

            string surl = GetUrl(2);
            surl = surl + "Store/GetAllStoreListPanel";
            StoreMasterModelRootObject sobj = new StoreMasterModelRootObject();
            //List<MenuMasterModel> mlist = new List<MenuMasterModel>();
            using (HttpClient sclient = new HttpClient())
            {
                HttpResponseMessage responseMessage = await sclient.GetAsync(surl);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var result = responseMessage.Content.ReadAsStringAsync().Result;
                    sobj = JsonConvert.DeserializeObject<StoreMasterModelRootObject>(result);
                    IList<SelectListItem> sSelectList = new List<SelectListItem>();
                    foreach (var item in sobj.data)
                    {
                        sSelectList.Add(new SelectListItem { Text = item.StoreName, Value = item.StoreId.ToString() });
                    }
                    sSelectList.Insert(0, new SelectListItem() { Value = "", Text = "Select Store" });
                    sSelectList.Insert(1, new SelectListItem() { Value = "0", Text = "All" });
                    ViewBag.StoreList = sSelectList;
                }
            }
            return View(new MenuMasterModel());
        }

        [HttpPost]
        public async Task<ActionResult> AddNewMenu(FormCollection fc, HttpPostedFileBase file)
        {
            //ViewBag.StoreId = Session["StoreId"].ToString();
            if (ModelState.IsValid)
            {
                int MenuId = 0;
                //string storeid = "";
                MenuMasterModel model = new MenuMasterModel();
                string menuid = fc["MenuId"];
                string menuname = fc["MenuName"];
                string menuprice = fc["MenuPrice"];
                //string storeid= fc["StoreName"];
                //if (ViewBag.StoreId == "0")
                //{
                //    storeid = fc["StoreName"];
                //}
                //else
                //{
                //    storeid = ViewBag.StoreId;
                //}

                string url = GetUrl(2);
                url = url + "Menu/AddNewMenu?menuid=" + menuid + "&menuname=" + menuname + "&menuprice=" + menuprice + "";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await client.GetAsync(url);
                    MenuMasterModelSignleRootObject result = new MenuMasterModelSignleRootObject();
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var response = responseMessage.Content.ReadAsStringAsync().Result;
                        var settings = new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            MissingMemberHandling = MissingMemberHandling.Ignore
                        };
                        result = JsonConvert.DeserializeObject<MenuMasterModelSignleRootObject>(response);
                        if (result.data.MenuId == 0)
                        {
                            MenuId = Convert.ToInt32(menuid);
                        }
                        else
                        {
                            MenuId = result.data.MenuId;
                        }
                        if (MenuId > 0)
                        {
                            try
                            {
                                var allowedExtensions = new[]
                                {
                                 ".Jpg", ".png", ".jpg", "jpeg",".JPG",
                            };
                                //string imagepath = "http://103.233.79.234/Data/ShamSweets_Android/LocalityPictures/";
                                model.ImageUrl = file.ToString(); //getting complete url                         
                                var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                                var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                                if (allowedExtensions.Contains(ext)) //check what type of extension  
                                {
                                    string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                                    string myfile = +MenuId + ext; //appending the name with id  
                                                                   // store the file inside ~/project folder(Img)  
                                                                   //var path = Path.Combine(imagepath, myfile);
                                    string path = @"C:\inetpub\wwwroot\Data\StoreFeedback_Android\MenuPictures\" + Server.HtmlEncode(myfile);
                                    model.ImageUrl = path;
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
                        TempData["message"] = "New Menu Added Successfully!";                        
                    }
                    else
                    {
                        TempData["message"] = "New Menu Not Added Successfully!";
                    }                    
                    return RedirectToAction("Index");
                }
            }
            else
            {
                TempData["message"] = "New Menu Not Added Successfully!";
                return RedirectToAction("Index");
            }
        }

    }
}