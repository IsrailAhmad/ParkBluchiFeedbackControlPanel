using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DropBoxApplication.Models
{
    public class MenuMasterModel
    {
        //[Required(ErrorMessage = "Store Name is required")]
        //public string StoreName { get; set; }
        //public int StoreId { get; set; }
        public int MenuId { get; set; }
        [Required(ErrorMessage = "Menu Name is required")]
        public string MenuName { get; set; }
        [Required(ErrorMessage = "Menu Price is required")]
        public decimal MenuPrice { get; set; }
        public string ImageUrl { get; set; }

    }
    public class MenuMasterModelRootObject
    {
        public List<MenuMasterModel> data { get; set; }

    }
    public class MenuMasterModelSignleRootObject
    {
        public MenuMasterModel data { get; set; }

    }
}