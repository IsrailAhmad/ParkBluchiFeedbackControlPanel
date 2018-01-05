using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DropBoxApplication.Models
{
    public class CustomerEntryModel
    {
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }

    public class CustomerEntryModelObjectRootModel
    {
       // public CustomerEntryModel data { get; set; }
        public Response data { get; set; }
    }
}