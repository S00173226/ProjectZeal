using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MVC5App.DTO;
using MVC5App.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MVC5App.Data
{
    public class DataAPIRepository : IDataAPIRepository
    {
        //Live Deployment Endpoint
        private readonly string APIEndPoint = "http://ec2-52-19-66-117.eu-west-1.compute.amazonaws.com:3000";
        //Local Testing Endpoint
        //private readonly string APIEndPoint = "http://localhost:3000";
        
        //Holding variable for List of Devices 
        private static IEnumerable<SelectListItem> deviceListStore;

        // API call to retrieve a list of Devices registered to a user
        public async Task<DisplayLocationViewModel> APIDevices(int UserID)
        {
            string devicesString;
            using (var client = new HttpClient())
            {
                string devicesJson;

                try
                {
                   var raw = client.GetStringAsync(APIEndPoint + "/getdevices/" + UserID).Result;
                    devicesJson = raw;
                    devicesString = devicesJson.ToString();
                    if (devicesString != null)
                    {
                        DisplayLocationViewModel model = JsonParser(devicesString, 1);
                        deviceListStore = model.Devices;
                        return model;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                
            }

            
            return new DisplayLocationViewModel();
        }
        //Method to allow deviceListStore to be accessed outside the repo
        public IEnumerable<SelectListItem> getDeviceliststore()
        {
            return deviceListStore;
        }

        //API request to retrieve Geolocation Data based on form results
        public async Task<DisplayLocationViewModel> APIGeoLocationRetrieval(DisplayLocationViewModel MapObj)
        {
            //Local Deploy
            string dateFormat = "dd,MM,yyyy";
            //AWS Deploy
            //string dateFormat = "MM,dd,yyyy";
            string timeFormat = "HH:mm:ss";
            string APIStartDateTime = MapObj.Start.ToString(dateFormat) + " " + MapObj.StartTime.ToString(timeFormat);
            string APIEndDateTime = MapObj.End.ToString(dateFormat) + " " + MapObj.EndTime.ToString(timeFormat);

            string locationData;
            using (var client = new HttpClient())
            {
                try
                {
                    var raw = client.GetStringAsync(APIEndPoint + "/getcoords/" + MapObj.UserID + "/" + MapObj.DeviceID + "/" + APIStartDateTime + "/" + APIEndDateTime).Result;
                    locationData = raw.ToString();
                    MapObj.JSONData = locationData;
                    MapObj.Devices = deviceListStore;
                    return MapObj;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            
        }

        // Requests the user id from API based on users email
        public async Task<int> APIUserRecordID(string UserEmail)
        {
            string dbUserID;
            int appUserID;
            using (var client = new HttpClient())
            {
                try
                {
                    var raw = client.GetStringAsync(APIEndPoint + "/user/" + UserEmail).Result;
                     dbUserID = raw.ToString();
                    appUserID = JsonParser(dbUserID, 2).UserID;
                    return appUserID;
                }
                catch
                {
                    //'0' Corrolates to User Does not exist in database
                    return 0;
                }
            }

            
        }

        //API call to create an entry for a user in the database
        public void UserCreation(RegisterViewModel registerViewModel)
        {
            if (APIUserRecordID(registerViewModel.Email).Result == 0)
            {
                using (var client = new HttpClient())
                {
                    try
                    {
                        StringContent stringContent = new StringContent(ConvertToJson(registerViewModel), Encoding.UTF8, "application/json");
                        string ApiPost = string.Format("{0}/user", APIEndPoint);
                        client.PostAsync(ApiPost, stringContent);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
        }

        //A json parser method to format the returned data into a readable object
        public DisplayLocationViewModel JsonParser(string json, int methodUsed)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            List<SelectListItem> devices = new List<SelectListItem>();
            dynamic listItems = JObject.Parse(json);
            int userID;


            switch (methodUsed)
            {
                case 1:
                    userID = listItems.data[0].User_ID;
                    
                    foreach (var item in listItems.data)
                    {
                
                        devices.Add(
                            new SelectListItem
                            {
                                Text = item.Device_Name,
                                Value = item.Device_ID
                            });
                        
                    }
                    
                    return new DisplayLocationViewModel(userID, new SelectList(devices, "Value", "Text")); ;


                case 2:
                    userID = listItems.data[0].User_ID;
                    return new DisplayLocationViewModel(userID);

                default:
                    break;
            }
            return new DisplayLocationViewModel();
        }

        //JSON parser to parse string into User Object
        public UserInfoModel JsonParser(string json)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
           
            dynamic listItems = JObject.Parse(json);
            UserInfoModel userInfo = new UserInfoModel();
            userInfo.UserID = listItems.data[0].User_ID;
            userInfo.Name = String.Format("{0} {1}", listItems.data[0].First_Name, listItems.data[0].Last_Name);
            if (userInfo.Name == null)
                userInfo.Name = "John Doe";
            userInfo.Address1 = listItems.data[0].Address_1;
            if (userInfo.Address1 == null)
                userInfo.Address1 = "No Address Line";
            userInfo.Address2 = listItems.data[0].Address_2;
            if (userInfo.Address2 == null)
                userInfo.Address2 = "No Address Line";
            userInfo.Address3 = listItems.data[0].Address_3;
            if (userInfo.Address3 == null)
                userInfo.Address3 = "No Address Line";
            userInfo.UserEmail = listItems.data[0].Email;

            userInfo.ContactNo = listItems.data[0].Contact_No;
            if (userInfo.ContactNo == null)
                userInfo.ContactNo = "No Contact Number";
            return userInfo;
        }

        //JSON convert to format a user's info into a structured JSON object to be sent to API
        public string ConvertToJson(RegisterViewModel registerViewModel)
        {
            UserCreateDTO user = new UserCreateDTO
            {
                roleID = 1,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.SecondName,
                Address1 = registerViewModel.Address1,
                Address2 = registerViewModel.Address2,
                Address3 = registerViewModel.Address3,
                County = registerViewModel.County,
                ContactNo = registerViewModel.ContactNo,
                email = registerViewModel.Email
            };
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                //TypeNameHandling = TypeNameHandling.Objects,
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                
            };
            string json = JsonConvert.SerializeObject(user, Formatting.Indented, settings);
            return json;
        }

        //API call to retrieve all data pertaining to a user
        public UserInfoModel APIUserInfo(int UserID)
        {
            string devicesString;
            using (var client = new HttpClient())
            {
                string userInfoJson;

                try
                {
                    var raw = client.GetStringAsync(APIEndPoint + "/userInfo/" + UserID).Result;
                    userInfoJson = raw;
                    devicesString = userInfoJson.ToString();
                    if (userInfoJson != null)
                    {
                        UserInfoModel model = JsonParser(userInfoJson);
                        model.Devices = APIDevices(UserID).Result.Devices;
                        
                        return model;
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

            }

            return new UserInfoModel();
            
        }
        
    }
    }