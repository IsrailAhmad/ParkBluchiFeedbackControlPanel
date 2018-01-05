using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DropBoxApplication.Models
{
    public class MenuSetUpViewModel
    {
        public int id { get; set; }
        public string MenuName { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class MenuSetUpViewModelObjectRootModel
    {
        public List<MenuSetUpViewModel> data { get; set; }
        public Response response { get; set; }
    }
}