using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team7ADProjectMVC.Models;

namespace Team7ADProjectMVC.TestControllers
{
    public class FCMController : Controller
    {
        // GET: FCM
        public ActionResult Index()
        {
            return View();
        }

        public void Test()
        {
            PushNotification fcmPush = new PushNotification();
            fcmPush.PushFCMNotification("Test", "Hello World");
        }
    }
}