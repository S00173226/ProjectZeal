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




namespace MVC5App.Controllers
{
    public class MapController : Controller
    {
        private string dbCon = "Datasource=year3project.ceryng3iqugy.eu-west-1.rds.amazonaws.com;Initial Catalog='3rdYearProject';port=3306;username=Administrator;password=Administrator";
        // GET: Map
        //[Route ("Map/LocationDisplay")]
        //public ActionResult LocationDisplay()
        //{
        //    int user = 1;

        //    var db = new DatabaseContext();
        //    var deviceslist = from u in db.Geolocations
        //                  join d in db.Devices on u.DeviceID equals d.DeviceId
        //                  where u.UserID == 2
        //                  select new MapLocationOBJ
        //                  {
        //                      DeviceID = u.DeviceID,
        //                      MacAddress = d.macAddress,
        //                      DateAndTimeRecorded = u.DateTimeRecorded,
        //                      Longitude = u.Longitude,
        //                      Latitude = u.Latitude
        //                  };
        //    List<MapLocationOBJ> results = deviceslist.ToList();

        //    DeviceJsonParser(results);

        //    var model = new DisplayLocationViewModel
        //        {

        //            //Devices = GetDevices(user)
        //            //Devices = GetDevices2(results)
        //            Devices = getDeviceList(results)
        //        };
        //        return View(model);

        //}
        //[Route("Map/LocationDisplay/RetrieveUserDevices")]
        public ActionResult Devices(int? UserID = null)
        {
            if (UserID == null)
            {
                return Redirect("/Map/UserNotFound");
            }

            try
            {
                var model = new DisplayLocationViewModel
                {
                    Devices = DatabaseDeviceRetrieval(UserID ?? 1),
                    Start = DateTime.Today.ToShortDateString(),
                    End = DateTime.Today.ToShortDateString()                 
                    
                };

                return View(model);
            }
            catch
            {
                return View();
            }
            

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

        private IEnumerable<SelectListItem> GetDevices(int UserAccount)
        {
            var db = new DatabaseContext();
            var devices = from u in db.Geolocations
                          join d in db.Devices on u.DeviceID equals d.DeviceId
                          where u.UserID == UserAccount
                          select new SelectListItem()
                          {
                              Value = d.DeviceId.ToString(),
                              Text = d.macAddress
                          };
            //var devices = db.Devices.Select(x => new SelectListItem
            //{
            //    Value = x.DeviceId.ToString(),
            //    Text = x.macAddress
            //});

            return new SelectList(devices, "Value", "Text");
        }

        private IEnumerable<SelectListItem> GetDevices2(List<MapLocationOBJ> results)
        {
            foreach (var item in results)
            {
                new SelectListItem()
                {
                    Value = item.DeviceID.ToString(),
                    Text = item.MacAddress
                };
            }
            return new SelectList(results, "Value", "Text");
        }

        private SelectList getDeviceList(List<MapLocationOBJ> results)
        {
            SelectList DeviceList = new SelectList(
                results.Select(d =>
                new { DeviceID = d.DeviceID,
                    MacAddress = d.MacAddress })
                      , "DeviceID", "MacAddress");
            return DeviceList;
        }

        //Converts C# List of Device locations into readable json format for the GoogleMaps Javascript to read
        private string DeviceJsonParser(List<MapLocationOBJ> DeviceLocations)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

                string json = JsonConvert.SerializeObject(DeviceLocations, Formatting.Indented, settings);

            return json;
        }

        public ActionResult UserNotFound()
        {
            return View();
        }


    }
}