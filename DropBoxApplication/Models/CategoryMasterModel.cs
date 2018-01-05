using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DropBoxApplication.Models
{
    public class CategoryMasterModel
    {
        //public int MenuId { get; set; }
        //[Required(ErrorMessage = "Menu Name is required")]
        //public string MenuName { get; set; }
        //public int StoreId { get; set; }
        //[Required(ErrorMessage = "Store Name is required")]
        //public string StoreName { get; set; }
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Category Name is required")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Category Description is required")]
        //public string CategoryDescription { get; set; }
        public string CategoryPictures { get; set; }
    }

    public class CategoryMasterModelRootObject
    {
        public List<CategoryMasterModel> data { get; set; }
        //public Response response { get; set; }
    }

    public class CategoryMasterModelSingleRootObject
    {
        public CategoryMasterModel data { get; set; }
        //public Response response { get; set; }
    }
}