using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DropBoxApplication.Models
{

    public class StoreMasterModel
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string StorePhoneNumber { get; set; }
        public string StoreEmailId { get; set; }
        public string StoreAddress { get; set; }
        public string StorePicturesUrl { get; set; }
    }

    public class StoreMasterModelRootObject
    {
        public List<StoreMasterModel> data { get; set; }
        public Response response { get; set; }

    }
    public class StoreMasterModelSignleRootObject
    {
        public StoreMasterModel data { get; set; }
        public Response response { get; set; }

    }
   
}
