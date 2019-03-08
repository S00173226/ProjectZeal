using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5App.Models
{
    public class UserInfoModel
    {
        public int UserID { get; set; }
        public string UserEmail { get; set; }
        public IEnumerable<SelectListItem> Devices { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }

        public UserInfoModel(int userID, IEnumerable<SelectListItem> devices)
        {
            UserID = userID;
            Devices = devices;
        }
        public UserInfoModel()
        {

        }
    }

}