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
            var disbursementList = from d in db.DisbursementLists
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
                DateTime selected = new DateTime(Int32.Parse((datesplit[2])), Int32.Parse((datesplit[1])), Int32.Parse((datesplit[0])));
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
                DateTime selected = new DateTime(Int32.Parse((datesplit[2])), Int32.Parse((datesplit[1])), Int32.Parse((datesplit[0])));
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
        public void ConfirmDisbursement(int? disburseid)
        {
            var deptid = db.DisbursementLists.Find(disburseid).DepartmentId;
            int rid = db.DisbursementLists.Find(disburseid).Retrieval.RetrievalId;
            List<Requisition> requisitionlist = db.Requisitions.Where(model => model.RetrievalId == rid).ToList();


            List<RequisitionDetail> rdlist = (from x in db.RequisitionDetails
                          where x.Requisition.RetrievalId == rid
                          && x.Requisition.DepartmentId == deptid
                          select x).ToList();

            var itlist = (from x in rdlist

                          group x by x.ItemNo into g
                          select new
                          {
                              ItemNo = g.Key,
                              OutstandingQuantity = g.Sum(x => x.OutstandingQuantity)
                          }).ToList();

            foreach (var total in itlist)

            {
                //var total = (from x in rdlist
                //             where x.ItemNo == item.ItemNo
                //             select x.OutstandingQuantity).Sum();

                //var total = rdlist.Where(x => x.ItemNo == item.ItemNo).Sum(y => y.OutstandingQuantity);
                var deliveryquantity = db.DisbursementLists.Find(disburseid).DisbursementDetails.Single(model => model.ItemNo == total.ItemNo).DeliveredQuantity;

                var samelist = (from x in rdlist
                                where x.ItemNo == total.ItemNo
                                orderby x.Requisition.RequisitionId
                                select x
                                ).ToList();


                // var deliveryquantity = db.DisbursementLists.Find(disburseid).DisbursementDetails.Single(model => model.ItemNo == item.ItemNo).DeliveredQuantity;
                if (total.OutstandingQuantity <= deliveryquantity)
                {
                    foreach (var item in samelist)
                    {
                        db.RequisitionDetails.Find(item.RequisitionDetailId).OutstandingQuantity = 0;
                    }
                }
                else

                {
                    //var samelist = (from x in rdlist
                    //                where x.ItemNo == item.ItemNo
                    //                orderby x.Requisition.RequisitionId
                    //                select x
                    //                ).ToList();

                    for (int i = 0; i < samelist.Count(); i++)
                    {

                        if (samelist[i].OutstandingQuantity > deliveryquantity)
                        {
                            db.RequisitionDetails.Find(samelist[i].RequisitionDetailId).OutstandingQuantity -= deliveryquantity;
                            break;
                        }
                        else
                        {
                            deliveryquantity -= samelist[i].OutstandingQuantity;
                            db.RequisitionDetails.Find(samelist[i].RequisitionDetailId).OutstandingQuantity = 0;

                        }


                    }


                }


            }
            db.DisbursementLists.Find(disburseid).Status = "Completed";

            foreach (var item in requisitionlist)
            {

                item.RequisitionStatus = "Completed";

            }

            foreach (var item in rdlist)
            {


                if (item.OutstandingQuantity != 0)
                {
                    item.Requisition.RequisitionStatus = "Outstanding";
                    break;

                }
            }

            db.SaveChanges();


        }

    }
}