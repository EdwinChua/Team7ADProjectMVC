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
            x.PushFCMNotification("drKO6q4iG2s:APA91bElDeQrhBpFWaHjB70oY8K95bnbKem4BpzWQnMeElN5TAVsPuleRRL6cOm1Mu9gSdCffdU1cSJUD9zJCQQ4waSzVDjbawTmIAr3wNvxtIV88VDv2iltofib61CD2OwucAS7xEsC" ,"hi linda");
            //return RedirectToAction("Index");
        }
    }
}