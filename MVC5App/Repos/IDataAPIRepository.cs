using MVC5App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVC5App.Repos
{
    public interface IDataAPIRepository
    {
        Task<string> Devices(string UserID);

        List<MapLocationOBJ> GeoLocationRetrieval(DisplayLocationViewModel MapObj, string UserID);

    }
}
