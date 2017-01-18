using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team7ADProjectMVC;

namespace Team7ADProjectMVC.TestControllers
{
    public class DisbursementDetailsController : Controller
    {
        private ProjectEntities db = new ProjectEntities();

        // GET: DisbursementDetails
        public ActionResult Index()
        {
            var disbursementDetails = db.DisbursementDetails.Include(d => d.DisbursementList).Include(d => d.RequisitionDetail);
            return View(disbursementDetails.ToList());
        }

        // GET: DisbursementDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisbursementDetail disbursementDetail = db.DisbursementDetails.Find(id);
            if (disbursementDetail == null)
            {
                return HttpNotFound();
            }
            return View(disbursementDetail);
        }

        // GET: DisbursementDetails/Create
        public ActionResult Create()
        {
            ViewBag.DisbursementListId = new SelectList(db.DisbursementLists, "DisbursementListId", "Status");
            ViewBag.RequisitionDetailId = new SelectList(db.RequisitionDetails, "RequisitionDetailId", "ItemNo");
            return View();
        }

        // POST: DisbursementDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DisbursementDetailId,DisbursementListId,RequisitionDetailId,Quantity,Remark,Remark1")] DisbursementDetail disbursementDetail)
        {
            if (ModelState.IsValid)
            {
                db.DisbursementDetails.Add(disbursementDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DisbursementListId = new SelectList(db.DisbursementLists, "DisbursementListId", "Status", disbursementDetail.DisbursementListId);
            ViewBag.RequisitionDetailId = new SelectList(db.RequisitionDetails, "RequisitionDetailId", "ItemNo", disbursementDetail.RequisitionDetailId);
            return View(disbursementDetail);
        }

        // GET: DisbursementDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisbursementDetail disbursementDetail = db.DisbursementDetails.Find(id);
            if (disbursementDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.DisbursementListId = new SelectList(db.DisbursementLists, "DisbursementListId", "Status", disbursementDetail.DisbursementListId);
            ViewBag.RequisitionDetailId = new SelectList(db.RequisitionDetails, "RequisitionDetailId", "ItemNo", disbursementDetail.RequisitionDetailId);
            return View(disbursementDetail);
        }

        // POST: DisbursementDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DisbursementDetailId,DisbursementListId,RequisitionDetailId,Quantity,Remark,Remark1")] DisbursementDetail disbursementDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(disbursementDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DisbursementListId = new SelectList(db.DisbursementLists, "DisbursementListId", "Status", disbursementDetail.DisbursementListId);
            ViewBag.RequisitionDetailId = new SelectList(db.RequisitionDetails, "RequisitionDetailId", "ItemNo", disbursementDetail.RequisitionDetailId);
            return View(disbursementDetail);
        }

        // GET: DisbursementDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DisbursementDetail disbursementDetail = db.DisbursementDetails.Find(id);
            if (disbursementDetail == null)
            {
                return HttpNotFound();
            }
            return View(disbursementDetail);
        }

        // POST: DisbursementDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DisbursementDetail disbursementDetail = db.DisbursementDetails.Find(id);
            db.DisbursementDetails.Remove(disbursementDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
