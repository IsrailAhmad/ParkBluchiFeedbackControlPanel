using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DropBoxApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace DropBoxApplication.Models
{
    public class ProductMasterModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Product Name is required")]
        public string ProductName { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Locality Name is required")]
        //public string LocalityName { get; set; }
        //[Required(ErrorMessage = "Store Name is required")]
        //public string StoreName { get; set; }
        //[Required(ErrorMessage = "Menu Name is required")]
        //public string MenuName { get; set; }
        //[Required(ErrorMessage = "Category Name is required")]
        //public string CategoryName { get; set; }
        //public int LocalityId { get; set; }
        //public int StoreId { get; set; }
        //public int MenuId { get; set; }
        //public int CategoryId { get; set; }
        //[Required(ErrorMessage = "Unit Price is required")]
        public decimal Price { get; set; }
        //[Required(ErrorMessage = "GST is required")]
        //public decimal GST { get; set; }
        //[Required(ErrorMessage = "Discount is required")]
        //public decimal Discount { get; set; }
        //[Required(ErrorMessage = "Tax Type is required")]
        //public string TaxType { get; set; }
        //[Required(ErrorMessage = "UOM is required")]
        //public string UOM { get; set; }
        //public string ProductDetails { get; set; }
        //[Required(ErrorMessage = "Lock is required")]
        public bool FoodType { get; set; }
        public string ProductPicturesUrl { get; set; }
        //public decimal DeliveryCharge { get; set; }
    }

    public class ProductMasterModelRootObject
    {
        public List<ProductMasterModel> data { get; set; }
    }

    public class ProductMasterModelSingleRootObject
    {
        public ProductMasterModel data { get; set; }
    }
}