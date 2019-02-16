using MVC5App.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using TestDB;
using System.Configuration;
using System.Data;
using System.Globalization;
using Newtonsoft.Json.Linq;


namespace MVC5App.Controllers
{
    public class MapController : Controller
    {


        private string dbCon = "Datasource=year3project.ceryng3iqugy.eu-west-1.rds.amazonaws.com;Initial Catalog='3rdYearProject';port=3306;username=Administrator;password=Administrator";

        private DisplayLocationViewModel model;
        public ActionResult Devices(int? Id)


        {
            //Checks If User has been logged in and valid
            if (Id == null)
            {
                return RedirectToAction("UserNotFound");
            }

            try
            {
                model = new DisplayLocationViewModel
                {
                    Devices = DatabaseDeviceRetrieval(Id ?? 1),
                    Start = DateTime.Today,
                    End = DateTime.Today,
                    UserID = Id ?? 1,
                    JSONData = "[{\r\n   \"lng\":-7.646063 ,\r\n    \"lat\": 54.347592\r\n  }\r\n]",
                    



                };
                ViewBag.DevicesModel = model;
                return View(model);
            }
            catch
            {
                return View();
            }
            

        }
        [HttpPost]
        public ActionResult Devices(DisplayLocationViewModel FormModel)
        {
            if (ModelState.IsValid)
            {  
                try
                {
                    model = new DisplayLocationViewModel
                    {
                        Devices = DatabaseDeviceRetrieval(1),
                        Start = DateTime.Today,
                        End = DateTime.Today,
                        UserID = 1,
                        JSONData = DeviceJsonParser(GeoLocationRetrieval(FormModel))
                        
                    
                    };

                    

                    return View(model);
                    // return RedirectToAction("Test", FormModel);
                }
                catch
                {
                    model = new DisplayLocationViewModel
                    {
                        Devices = DatabaseDeviceRetrieval(1),
                        Start = DateTime.Today,
                        End = DateTime.Today,
                        UserID = 1,
                        JSONData = "[{\r\n   \"lng\":-7.646063 ,\r\n    \"lat\": 54.347592\r\n  }\r\n]",

                    };
                    return View();
                }
                
                
            }
            model = new DisplayLocationViewModel
            {
                Devices = DatabaseDeviceRetrieval(1),
                Start = DateTime.Today,
                End = DateTime.Today,
                UserID = 1,
                JSONData = "[{\r\n   \"lng\":-7.646063 ,\r\n    \"lat\": 54.347592\r\n  }\r\n]",

            };
            return View();
        }

        //[HttpGet]
        //public async Task<string> Test(DisplayLocationViewModel FormModel)
        //{
        //    var data = GeoLocationRetrieval(FormModel);
            
        //    return (DeviceJsonParser(await data));
        //}

        public ActionResult UserNotFound()
        {
            return View();
        }

        public ActionResult MapForm()
        {
            //ViewBag.Locations = DeviceJsonParser(GeoLocationRetrieval(FormModel));
            return View();
        }

        

        private SelectList DatabaseDeviceRetrieval(int UserId)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            MySqlParameter user_ID = new MySqlParameter("userid", MySqlDbType.Int32);
            MySqlConnection connection = new MySqlConnection(dbCon);


            user_ID.Value = UserId;
            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Return_Device_Names";

            command.Parameters.Add(user_ID);
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    items.Add(new SelectListItem
                    {
                        Text = reader["Device_Name"].ToString(),
                        Value = reader["Device_ID"].ToString()
                    });


                    
                }
            }
            connection.Close();
            return  new SelectList(items, "Value" , "Text");
        }

        private MapLocationOBJ[] GeoLocationRetrieval(DisplayLocationViewModel MapObj)
        {
            List<MapLocationOBJ> items = new List<MapLocationOBJ>();
            MySqlParameter userid = new MySqlParameter("userid", MySqlDbType.Int32);
            MySqlParameter deviceid = new MySqlParameter("deviceid", MySqlDbType.Int32);
            MySqlParameter startdatetime = new MySqlParameter("startdatetime", MySqlDbType.DateTime);
            MySqlParameter enddatetime = new MySqlParameter("enddatetime", MySqlDbType.DateTime);
            MySqlConnection connection = new MySqlConnection(dbCon);

            string dateFormat = "yyyy-MM-dd";
            string timeFormat = "HH:mm:ss";
            userid.Value = 1;
            deviceid.Value = MapObj.DeviceID;
            string MySQLStartDateTime = MapObj.Start.ToString(dateFormat) + " " + MapObj.StartTime.ToString(timeFormat);
            startdatetime.Value = MySQLStartDateTime;
            string MySQLEndDateTime = MapObj.End.ToString(dateFormat) + " " + MapObj.EndTime.ToString(timeFormat);
            enddatetime.Value = MySQLEndDateTime;

            MySqlCommand command = new MySqlCommand();
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "Retrieve_Location_Data";
            command.Parameters.Add(userid);
            command.Parameters.Add(deviceid);
            command.Parameters.Add(startdatetime);
            command.Parameters.Add(enddatetime);

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();

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
            connection.Close();
            MapLocationOBJ[] MapLocations = new MapLocationOBJ[items.Count];

            for (int i = 0; i < MapLocations.Length; i++)
            {
                MapLocations[i] = items[i];
            }
                        
            return MapLocations;
        }

        //Converts C# List of Device locations into readable json format for the GoogleMaps Javascript to read
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