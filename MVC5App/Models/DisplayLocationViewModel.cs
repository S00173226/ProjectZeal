using Newtonsoft.Json.Linq;
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
        public int UserID { get; set; }
        public Decimal Latitude { get; set; }
        public Decimal Longitude { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Start { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime End { get; set; }
        public string JSONData { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
