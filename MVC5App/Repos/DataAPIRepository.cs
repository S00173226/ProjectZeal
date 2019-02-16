using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MVC5App.Models;
using Newtonsoft.Json;

namespace MVC5App.Repos
{
    public class DataAPIRepository : IDataAPIRepository
    {
        public async Task<string> Devices(string UserID)
        {
            string devicesJson = await /*Api Call */



            return devicesJson;
        }

        public List<MapLocationOBJ> GeoLocationRetrieval(DisplayLocationViewModel MapObj, string UserID)
        {
            List<MapLocationOBJ> items = new List<MapLocationOBJ>();
            /* API Call Here*/

            string dateFormat = "yyyy-MM-dd";
            string timeFormat = "HH:mm:ss";
            userid.Value = UserID;
            deviceid.Value = MapObj.DeviceID;
            string MySQLStartDateTime = MapObj.Start.ToString(dateFormat) + " " + MapObj.StartTime.ToString(timeFormat);
            startdatetime.Value = MySQLStartDateTime;
            string MySQLEndDateTime = MapObj.End.ToString(dateFormat) + " " + MapObj.EndTime.ToString(timeFormat);
            enddatetime.Value = MySQLEndDateTime;



            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    items.Add(new MapLocationOBJ
                    {
                        DeviceID = int.Parse(reader["Device_ID"].ToString()),
                        DateAndTimeRecorded = reader["Date_Time_Recorded"].ToString(),
                        Latitude = decimal.Parse(reader["Latitude"].ToString()),
                        Longitude = decimal.Parse(reader["Longitude"].ToString()),


                    });



                }
            }

            MapLocationOBJ[] MapLocations = new MapLocationOBJ[items.Count];

            for (int i = 0; i < MapLocations.Length; i++)
            {
                MapLocations[i] = items[i];
            }

            return MapLocations.ToList();
        }

        private string DeviceJsonParser(MapLocationOBJ[] DeviceLocations)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore
            };




            string json = JsonConvert.SerializeObject(DeviceLocations, Formatting.Indented, settings);

            if (json == "[]")
            {
                json = "[{\r\n   \"lng\":-7.646063 ,\r\n    \"lat\": 54.347592\r\n  }\r\n]";
            }


            return json;
        }
    }
}