using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DropBoxApplication.Models
{

    public class LocalityMasterModel
    {
        public int LocalityId { get; set; }
        public string LocalityName { get; set; }
        public object ImageUrl { get; set; }
    }

    public class LocalityMasterModelRootObject
    {
        public List<LocalityMasterModel> data { get; set; }
    }
}