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
            PushNotification x = new PushNotification();
            x.PushFCMNotification("cnumUhgAHUc:APA91bG8NM8UjP0wZWl8em22TCxw3btnfzNvLhBzv6MZJ9snAwvgsunFsyDvE7e9bSzfQzOGdX3HXjJW_32xSxsmu70gjkFnvhbGU8cnj6ZT6__UVoWEHJF0OgdYKgVO19NOKYE_vyot", "hi linda");
            //return RedirectToAction("Index");
        }
    }
}