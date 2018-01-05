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
    public class ProductMasterController : BaseController
    {
        // GET: ProductMaster
        public ActionResult Index()
        {
            ViewBag.LoginID = Session["LoginID"].ToString();
            ViewBag.Username = Session["Username"].ToString();
            ViewBag.Message = @TempData["message"];
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddNewProduct(FormCollection fc, HttpPostedFileBase file)
        {
            //if (ModelState.IsValid)
            //{
            int ProductId = 0;
            ProductMasterModel model = new ProductMasterModel();
            string productId = fc["ProductId"];
            string CategoryName = fc["CategoryName"];
            string ProductName = fc["ProductName"];
            string Price = fc["Price"];
            //string GST = fc["GST"];
            //string Discount = fc["Discount"];
            //string TaxType = fc["TaxType"];
            string FoodType = Convert.ToString(fc["FoodType"].Split(',')[0]); ;
            //string UOM = fc["UOM"];
            //string ProductDetails = fc["ProductDetails"];
            //string DeliveryCharge = fc["DeliveryCharge"];

            //string ProductId = Convert.ToString(model.ProductId);
            //string ProductDetails = model.ProductDetails;
            //string CategoryName = model.CategoryName;
            //string ProductName = model.ProductName;
            //string UnitPrice = Convert.ToString(model.UnitPrice);
            //string GST = Convert.ToString(model.GST);
            //string Discount = Convert.ToString(model.Discount);
            //string TaxType = Convert.ToString(model.TaxType);
            ////bool Lock = Convert.ToBoolean(fc["Lock"].Split(',')[0]); ;
            //bool Lock = model.Lock;
            //string UOM = Convert.ToString(model.UOM);
            ////string ProductDetails = fc["ProductDetails"];
            string url = GetUrl(2);
            url = url + "ProductMaster/AddNewProduct?ProductId=" + productId + "&ProductName=" + ProductName + "&Price=" + Price + "&FoodType=" + FoodType + "";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync(url);
                ProductMasterModelSingleRootObject result = new ProductMasterModelSingleRootObject();
                if (responseMessage.IsSuccessStatusCode)
                {
                    var response = responseMessage.Content.ReadAsStringAsync().Result;
                    var settings = new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        MissingMemberHandling = MissingMemberHandling.Ignore
                    };
                    result = JsonConvert.DeserializeObject<ProductMasterModelSingleRootObject>(response, settings);
                    if (result.data.ProductId == 0)
                    {
                        ProductId = Convert.ToInt32(productId);
                    }
                    else
                    {
                        ProductId = result.data.ProductId;
                    }
                    if (ProductId > 0)
                    {
                        try
                        {
                            var allowedExtensions = new[]
                            {
                                 ".Jpg", ".png", ".jpg", "jpeg",".JPG",
                            };
                            //string imagepath = "http://103.233.79.234/Data/ShamSweets_Android/LocalityPictures/";
                            model.ProductPicturesUrl = file.ToString(); //getting complete url                         
                            var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                            var ext = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)  
                            if (allowedExtensions.Contains(ext)) //check what type of extension  
                            {
                                string name = Path.GetFileNameWithoutExtension(fileName); //getting file name without extension  
                                string myfile = +ProductId + ext; //appending the name with id  
                                                                  // store the file inside ~/project folder(Img)  
                                                                  //var path = Path.Combine(imagepath, myfile);
                                string path = @"C:\inetpub\wwwroot\Data\StoreFeedback_Android\ProductPictures\" + Server.HtmlEncode(myfile);
                                model.ProductPicturesUrl = path;
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
                TempData["message"] = "New Menu Added Successfully!";
                return RedirectToAction("Index");
            }
        }
    }
}