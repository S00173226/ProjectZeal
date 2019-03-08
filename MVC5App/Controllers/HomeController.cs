using MVC5App.Data;
using MVC5App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace MVC5App.Controllers
{
    public class HomeController : Controller
    {
        private DataAPIRepository dataRepo = new DataAPIRepository();
        private UserInfoModel userInfo;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var identity = (ClaimsIdentity)User.Identity;
            int? Id;
            try
            {
                Id = int.Parse(identity.FindFirst("UserID").Value);
            }
            catch
            {
                return RedirectToAction("UserNotFound", "Map");
            }

            try
            {
                userInfo = dataRepo.APIUserInfo(Id.Value);
            }
            catch (Exception e)
            {
                throw e;
            }
            return View(userInfo);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        
    }
}