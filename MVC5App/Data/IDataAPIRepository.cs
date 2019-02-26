using MVC5App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC5App.Data
{
    public interface IDataAPIRepository
    {
        Task<DisplayLocationViewModel> APIDevices(int UserID);

        Task<DisplayLocationViewModel> APIGeoLocationRetrieval(DisplayLocationViewModel MapObj);

        Task<int> APIUserRecordID(string UserEmail);


    }
}
