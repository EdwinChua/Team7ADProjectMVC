using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Team7ADProjectMVC.Services.DepartmentService;

namespace Team7ADProjectMVC.Models
{
    public class PushNotification
    {
        //IDepartmentService deptSvc;
        ProjectEntities db = new ProjectEntities();
        public PushNotification()
        {
            // TODO: Add constructor logic here
             //deptSvc = new DepartmentService();
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

        public PushNotification PushFCMNotification(string title, string message, string token,List<String> myData){
        
            PushNotification result = new PushNotification();
            try {
                result.Successful = true;
                result.Error = null;

                string SERVER_API_KEY = "AAAAjD8Iv20:APA91bG3BW0rQSG9WRbpf0SCSboeMOlwm9xyTZF3AsPNbj97wlM7resjGzdjUUQuhvytRdWsvoEKcwq4vKqMeM2uBQRLBj84tWxSWeX87XV1-p_DRBtBUrlxvt_Qq1tDrwFDo_9a9A5t";
                var SENDER_ID = "602352959341";
                WebRequest tRequest;
                tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));

                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

              var data = new
                   {
                       //single device
                       to = token,
               
                       notification = new
                        {
                            title = title,
                            body = message,                      
                       },
                      data = new
                       {
                          intent = myData[0],
                          pageHeader = myData[1],
                          id = myData[2],
                          extraDetail = myData[3],
                          //f4 = myData[4],
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

        public PushNotification PushFCMNotificationToStoreClerk(string title, string message, List<KeyValuePair<string, int>> myData)
        {

            PushNotification result = new PushNotification();
            
            var tokenList = from e in db.Employees
                            where e.RoleId == 1
                            && e.Token != null
                            select e.Token;
            foreach (var token in tokenList.ToList())
            {
                try
                {
                    result.Successful = true;
                    result.Error = null;

                    string SERVER_API_KEY = "AAAAjD8Iv20:APA91bG3BW0rQSG9WRbpf0SCSboeMOlwm9xyTZF3AsPNbj97wlM7resjGzdjUUQuhvytRdWsvoEKcwq4vKqMeM2uBQRLBj84tWxSWeX87XV1-p_DRBtBUrlxvt_Qq1tDrwFDo_9a9A5t";
                    var SENDER_ID = "602352959341";
                    WebRequest tRequest;
                    tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                    tRequest.Method = "post";
                    tRequest.ContentType = "application/json";
                    tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));

                    tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                    var data = new
                    {
                        //single device
                        to = token,

                        notification = new
                        {
                            title = title,
                            body = message,
                        },
                        data = new
                        {
                            myData,
                            
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
            }
            return result;
        }

        public void CheckForStockReorder()
        {
            var checkForStockReorder = (from x in db.Inventories
                                        where x.Quantity < x.ReorderLevel
                                        select x).ToList();
            if (checkForStockReorder != null)
            {
                var list = new List<KeyValuePair<string, int>>();
                foreach (var item in checkForStockReorder)
                {
                    list.Add(new KeyValuePair<string, int>(item.Description, (int)item.Quantity));
                }
                PushFCMNotificationToStoreClerk("Low Stock Alert", "Please see here", list);
            }
        }
}
}