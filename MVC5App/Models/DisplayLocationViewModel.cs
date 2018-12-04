using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC5App.Models
{
    public class DisplayLocationViewModel
    {
        [Display (Name = "NoOfDevices")]
        public IEnumerable<SelectListItem> Devices { get; set; }
        public int DeviceID { get; set; }
        public Decimal Latitude { get; set; }
        public Decimal Longitude { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
    }
}
