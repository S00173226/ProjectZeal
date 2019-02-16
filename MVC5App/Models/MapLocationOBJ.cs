using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5App.Models
{
    public class MapLocationOBJ
    {
        [JsonProperty("deviceID")]
        public int DeviceID { get; set; }
        [JsonProperty("deviceName")]
        public string DeviceName { get; set; }
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString ="{0:yyyy-MM-dd hh:mm:ss}")]
        [JsonProperty("date")]
        public string DateAndTimeRecorded { get; set; }
        [JsonProperty("lng")]
        public decimal Longitude { get; set; }
        [JsonProperty("lat")]
        public decimal Latitude { get; set; }
        
    }
}