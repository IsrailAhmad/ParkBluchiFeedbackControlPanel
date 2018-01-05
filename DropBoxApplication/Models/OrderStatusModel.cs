using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DropBoxApplication.Models
{
    //public class OrderStatusModel
    //{
    //    public int OrderStatusId { get; set; }
    //    public String OrderStatus { get; set; }
    //    public int TotalCount { get; set; }
    //}
    public class OrderStatusModel
    {
        public int OrderStatusId { get; set; }
        public string OrderStatus { get; set; }
        public int Count { get; set; }
    }
   

    public class OrderStatusRootObject
    {
        public List<OrderStatusModel> data { get; set; }
        public Response response { get; set; }
    }
}