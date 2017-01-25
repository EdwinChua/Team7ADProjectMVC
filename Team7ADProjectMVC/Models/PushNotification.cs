﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Team7ADProjectMVC.Models
{
    public class PushNotification
    {
        public PushNotification()
        {
            // TODO: Add constructor logic here
        }

        public bool Successful
        {
            get;
            set;
        }

        public string Response
        {
            get;
            set;
        }
        public Exception Error
        {
            get;
            set;
        }

        public PushNotification PushFCMNotification(string title, string message, string token)
        {
            PushNotification result = new PushNotification();
            try {
                result.Successful = true;
                result.Error = null;

                string SERVER_API_KEY = "AAAAjD8Iv20:APA91bG3BW0rQSG9WRbpf0SCSboeMOlwm9xyTZF3AsPNbj97wlM7resjGzdjUUQuhvytRdWsvoEKcwq4vKqMeM2uBQRLBj84tWxSWeX87XV1-p_DRBtBUrlxvt_Qq1tDrwFDo_9a9A5t";
                var SENDER_ID = "602352959341";
                var value = message;
                WebRequest tRequest;
                tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));

                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                var data = new
                {
                    // to = YOUR_FCM_DEVICE_ID, // Uncoment this if you want to test for single device
                    to = token, // this is for topic 
                    notification = new
                    {
                        title = title,
                        body = message,
                        //icon="myicon"
                    }
                };

                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);

                Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                tRequest.ContentLength = byteArray.Length;

                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse tResponse = tRequest.GetResponse();

                dataStream = tResponse.GetResponseStream();

                StreamReader tReader = new StreamReader(dataStream);

                String sResponseFromServer = tReader.ReadToEnd();
                result.Response = sResponseFromServer;

                tReader.Close();
                dataStream.Close();
                tResponse.Close();
            }
            catch (Exception e)
            {
                result.Successful = false;
                result.Response = null;
                result.Error = e;
            }
            return result;
        }
    }
}