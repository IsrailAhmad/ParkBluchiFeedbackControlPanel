using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DropBoxApplication.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? GST { get; set; }
        public decimal? Discount { get; set; }
        public string TaxType { get; set; }
        public string UOM { get; set; }
        public string HSN { get; set; }
        public string SweetsReset { get; set; }
        public string ProductDetails { get; set; }
        public string Lock { get; set; }
        public string ProductPicturesUrl { get; set; }
    }
}