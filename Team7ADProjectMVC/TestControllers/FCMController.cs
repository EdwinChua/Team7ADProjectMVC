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
            fcmPush.PushFCMNotification("Test", "Hello World", "fg_Zb3GAPYo:APA91bEbhMLwk_P2IlFEh13MeJaz6Tlf4dV2Gx1n9Apfx38JWRNMr8YY0ZktYw77IS31iO39H1hB22-t6OdXCC8AbSrxsVFivB6i2IOQbp1FaQpWRTEkzgRynsqEbwyVnPS8WfJgPE0W","Registrar");
        }

    }
}