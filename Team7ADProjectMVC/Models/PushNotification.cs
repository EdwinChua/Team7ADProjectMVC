﻿using System;
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

        public PushNotification PushFCMNotificationToStoreClerk(string title, string message, List<String> myData)
        {

            PushNotification result = new PushNotification();
            
            var tokenList = from e in db.Employees
                            where e.RoleId == 1
                            && e.Token != null
                            select e.Token;
            foreach (var token in tokenList.ToList())
            {
                PushFCMNotification(title, message,token, myData);
            }
            return result;
        }

        public void CheckForStockReorder()
        {
            var checkForStockReorder = (from x in db.Inventories
                                        where x.Quantity < x.ReorderLevel
                                        select x).ToList();

            List<String> myData = new List<string>();
            myData.Add("StockCard");
            myData.Add("StockCard");
            myData.Add("0");
            myData.Add("0");
            if (checkForStockReorder != null)
            {
                PushFCMNotificationToStoreClerk("Low Stock Alert", "Please see here", myData);
            }
        }

        public void CollectionPointChanged(String deptid,String  collectionptid)
        {

                int dId = Convert.ToInt32(deptid);
                int cpoint = Convert.ToInt32(collectionptid);
                Department wcfItem = db.Departments.Where(p => p.DepartmentId == dId).First();
                String cpointName = wcfItem.CollectionPoint.PlaceName;
                String deptname= wcfItem.DepartmentName;
                List<String> myData = new List<string>();
                myData.Add("DisbursementList");
                myData.Add("Disbursement List");
                myData.Add("0");
                myData.Add("0");

                PushFCMNotificationToStoreClerk(deptname+"'s Collection Point", "Changed to : "+ cpointName, myData);
               
        }

        public void RepAcceptRequisition(String DisListID)
        {

            int dlid = Convert.ToInt32(DisListID);
            var deptName = from d in db.DisbursementLists
                    where d.DisbursementListId == dlid
                    select d.Department.DepartmentName;
            List < String > myData = new List<string>();
            myData.Add("DisbursementList");
            myData.Add("Disbursement List");
            myData.Add("0");
            myData.Add("0");

            PushFCMNotificationToStoreClerk("Disbursement completed", deptName+ " accepted disbursement.", myData);
        }


}
}