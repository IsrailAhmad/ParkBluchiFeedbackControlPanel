using System.Web.Mvc;

namespace DropBoxApplication.App_Start
{
    public class BaseController : Controller
    {
        string url = string.Empty;
        public BaseController()
        {
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session != null)
            {
                var LoginID = filterContext.HttpContext.Session["LoginID"];
                base.OnActionExecuting(filterContext);
            }
        }
        public string GetUrl(int urlType)
        {
            if (urlType == 1)
                url = "http://localhost:56381/api/";
            else if (urlType == 2)
                //url = "http://localhost:51673/api/";
                url = "http://103.233.79.234:1010/api/";
            return url;
        }
    }
}