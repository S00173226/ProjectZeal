using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5App.DTO
{
    
    public class UserCreateDTO
    {
        [JsonProperty("roleID")]
        public int roleID { get; set; }
        [JsonProperty("FirstName")]
        public string FirstName { get; set; }
        [JsonProperty("LastName")]
        public string LastName { get; set; }
        [JsonProperty("Address1")]
        public string Address1 { get; set; }
        [JsonProperty("Address2")]
        public string Address2 { get; set; }
        [JsonProperty("Address3")]
        public string Address3 { get; set; }
        [JsonProperty("County")]
        public string County { get; set; }
        [JsonProperty("ContactNo")]
        public string ContactNo { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
    }
}