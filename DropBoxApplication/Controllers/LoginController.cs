using DropBoxApplication.App_Start;
using DropBoxApplication.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace DropBoxApplication.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Userlogin(UserLoginViewModel login, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                string url = GetUrl(2);
                url = url + "/WebSiteLogin/WebsiteLogin?username=" + login.UserName + "&password=" + login.Password + "";
                UserRootObject lRole = new UserRootObject();
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage responseMessage = await client.GetAsync(url);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        var result = responseMessage.Content.ReadAsStringAsync().Result;
                        lRole = JsonConvert.DeserializeObject<UserRootObject>(result);
                        if (lRole.response.isSuccess == true)
                        {
                            Session["LoginID"] = lRole.data.UserID;
                            Session["Username"] = lRole.data.FirstName + ' ' + lRole.data.LastName;
                            return RedirectToAction("Index", "MenuSetupMaster");
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username or password");
                        return View("Index", login);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Incorrect username and/or password");
            }
            return View(login);
        }
    }
}