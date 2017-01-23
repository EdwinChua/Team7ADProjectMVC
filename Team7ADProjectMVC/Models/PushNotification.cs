using System;
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
        public string PushFCMNotification(string ID, string message)
        {
            string SERVER_API_KEY = "AAAAFOhU39k:APA91bFfHD6bi8vruAKHvyFlTIJtJO2P0k94L7t9BhmdlvAEQAmBXEZfCzVGGp4FfK7ExWCtAkzqbsbok1DrsWzswOvYNFUQ_xjk_aR5IG6mvnv_pZHQ8l3g9-KLyQp-OD4Ht24jjmzm";
            var SENDER_ID = "89797222361";
            var value = message;
            WebRequest tRequest;
            tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";
            tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));

            tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

            var data = new
            {
                to = "drKO6q4iG2s:APA91bElDeQrhBpFWaHjB70oY8K95bnbKem4BpzWQnMeElN5TAVsPuleRRL6cOm1Mu9gSdCffdU1cSJUD9zJCQQ4waSzVDjbawTmIAr3wNvxtIV88VDv2iltofib61CD2OwucAS7xEsC",
                notification = new
                {
                    body = "This is the message",
                    title = "This is the title"
                    //icon = "myicon"
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


            tReader.Close();
            dataStream.Close();
            tResponse.Close();
            return sResponseFromServer;
        }

    }
}