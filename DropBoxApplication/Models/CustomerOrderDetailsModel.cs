using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DropBoxApplication.Models
{
   
    public class OrderDetail
    {
        public int OrderDetailsId { get; set; }
        public int OrderNumber { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public object ProductPicturesUrl { get; set; }
    }    

    public class CustomerOrderDetailsRootObject
    {
        public CustomerOrderModel data { get; set; }
        public Response response { get; set; }
    }

}