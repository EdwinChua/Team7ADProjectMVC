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

            //fcmPush.PushFCMNotification("Test", "Hello World", "epV-snZV_Rk:APA91bHSPzKm5TKZuwzTTaqxilfi3muF2WVF8BMMjy9G_1u1BDCdXRSDkSw25aR0NGgFOr15X8eedQQAQ9AF9Cl9-cHhuMWpC-NUVZrzpbhRKYaA0c4fnZFHlHF7JBgxyzbMF7MtT9Cj");

     //   fcmPush.RepAcceptRequisition("5");// call when rep accepts...

      //   fcmPush.CollectionPointChanged(2);// call when rep accepts...
        //    fcmPush.NewRequisitonMade("3");

        //    fcmPush.NotificationForHeadOnCreate("14");


        }

    }
}