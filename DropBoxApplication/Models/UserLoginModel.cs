using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DropBoxApplication.Models
{

    public class UserLoginModel
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public object Password { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class Response
    {
        public bool isSuccess { get; set; }
        public DateTime serverResponseTime { get; set; }
    }

    public class UserRootObject
    {
        public UserLoginModel data { get; set; }
        public Response response { get; set; }
    }

}