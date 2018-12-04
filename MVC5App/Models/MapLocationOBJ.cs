using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5App.Models
{
    public class MapLocationOBJ
    {
        public int DeviceID { get; set; }
        public string MacAddress { get; set; }
        public DateTime DateAndTimeRecorded { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}