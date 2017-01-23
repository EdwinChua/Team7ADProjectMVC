using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Team7ADProjectMVC.Services
{
    public class DisbursementService : IDisbursementService
    {
        ProjectEntities db = new ProjectEntities();


        public List<DisbursementList> GetAllDisbursements()
        {
            var disbursementList=from d in db.DisbursementLists
                                 orderby d.Status
                                 select d;

            return (disbursementList.ToList());
        }


        public DisbursementList GetDisbursementById(int? id)
        {

            return db.DisbursementLists.Find(id);
        }


        public List<DisbursementList> GetDisbursementsBySearchCriteria(int? departmentId, string status)
        {
            if ((status == null || status == "") && departmentId == null)
            {
                return (db.DisbursementLists.ToList());
            }
            else if (status == null || status == "")
            {
                var queryResults = from d in db.DisbursementLists
                                   where d.DepartmentId == departmentId
                                   orderby d.OrderedDate
                                   select d;
                return (queryResults.ToList());
            }
            else if (departmentId == null)
            {
                var queryResults = from d in db.DisbursementLists
                                   where d.Status == status
                                   orderby d.OrderedDate
                                   select d;
                return (queryResults.ToList());
            }
            else
            {
                var queryResults = from d in db.DisbursementLists
                                   where d.DepartmentId == departmentId
                                   && d.Status == status
                                   orderby d.OrderedDate
                                   select d;
                return (queryResults.ToList());
            }

        }

        public void UpdateDisbursementList(DisbursementList disbursementList)
        {
            db.Entry(disbursementList).State = EntityState.Modified;
            db.SaveChanges();
        }
        public List<DisbursementList> FindDisbursementsBySearch(string date, string status)
        {



            if ((status == null || status == "") && (date == null || date == ""))
            {
                return (db.DisbursementLists.ToList());
            }
            else if (status == null || status == "")
            {
                List<String> datesplit = date.Split('/').ToList<String>();
                DateTime selected = new DateTime(Int32.Parse((datesplit[0])), Int32.Parse((datesplit[1])), Int32.Parse((datesplit[2])));
                var queryResults = from d in db.DisbursementLists
                                   where d.DeliveryDate == selected
                                   orderby d.Status
                                   select d;
                return (queryResults.ToList());
            }
            else if (date == null || date == "")
            {
                var queryResults = from d in db.DisbursementLists
                                   where d.Status.Equals(status)
                                   orderby d.DeliveryDate
                                   select d;
                return (queryResults.ToList());
            }
            else
            {
                List<String> datesplit = date.Split('/').ToList<String>();
                DateTime selected = new DateTime(Int32.Parse((datesplit[0])), Int32.Parse((datesplit[1])), Int32.Parse((datesplit[2])));
                var queryResults = from d in db.DisbursementLists
                                   where d.Status.Equals(status)
                                   && d.DeliveryDate == selected
                                   orderby d.DeliveryDate
                                   select d;
                return (queryResults.ToList());
            }




        }

        public List<DisbursementDetail> GetdisbursementdetailById(int? id)
        {
            var disbursementDetails = db.DisbursementDetails.Where(model => model.DisbursementListId == id).Include(d => d.DisbursementList);
            return (disbursementDetails.ToList());

        }
        public string findCpnameByDisburse(int? id)
        {
            return (db.DisbursementLists.Find(id).Department.CollectionPoint.PlaceName);
        }
        public string findCptimeByDisburse(int? id)
        {
            return (db.DisbursementLists.Find(id).Department.CollectionPoint.CollectTime.ToString());
        }
        public string findDisbursenmentStatus(int? id)
        {
            return (db.DisbursementLists.Find(id).Status);
        }
        public void ConfirmDisbursement(int? id)
        {
            int rid = db.DisbursementLists.Find(id).Retrieval.RetrievalId;
            List<RequisitionDetail> rdlist = db.Requisitions.Single(model => model.RetrievalId == rid).RequisitionDetails.ToList();

            foreach (var item in rdlist)
            {
                db.RequisitionDetails.Find(item.RequisitionDetailId).OutstandingQuantity = item.Quantity-db.DisbursementLists.Find(id).DisbursementDetails.Single(model=>model.ItemNo==item.ItemNo).DeliveredQuantity;

            }
            db.DisbursementLists.Find(id).Status = "Completed";
            db.SaveChanges();

            
        }
        
    }
}