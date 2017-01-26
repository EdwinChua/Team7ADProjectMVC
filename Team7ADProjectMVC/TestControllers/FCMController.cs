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
            
            //fcmPush.PushFCMNotification("Test", "Hello World", "d7kbhWxRWes:APA91bFAw_SDOdBXmM4BuKwgA7VOFFjNwf11nD5XVMATfVJiV7KHklu96yMlt8QsNaTFnZvPo3KoLSmF5iMuC3AdDlIt-O4rnJwB_1mqHPzwOGTrrfLW6bIGar7-6onV9UJJ0h97Ivcv");
        }

    }
}