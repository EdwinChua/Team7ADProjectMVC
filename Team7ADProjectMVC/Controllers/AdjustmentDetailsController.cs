using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team7ADProjectMVC;

namespace Team7ADProjectMVC.Controllers
{
    public class AdjustmentDetailsController : Controller
    {
        private ProjectEntities db = new ProjectEntities();

        // GET: AdjustmentDetails
        public ActionResult Index()
        {
            var adjustmentDetails = db.AdjustmentDetails.Include(a => a.Inventory);
            return View(adjustmentDetails.ToList());
        }

        // GET: AdjustmentDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdjustmentDetail adjustmentDetail = db.AdjustmentDetails.Find(id);
            if (adjustmentDetail == null)
            {
                return HttpNotFound();
            }
            return View(adjustmentDetail);
        }

        // GET: AdjustmentDetails/Create
        public ActionResult Create()
        {
            ViewBag.ItemNo = new SelectList(db.Inventories, "ItemNo", "Description");
            return View();
        }

        // POST: AdjustmentDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdjustmentDetailId,AdjustmentId,ItemNo,Quantity,Reason")] AdjustmentDetail adjustmentDetail)
        {
            if (ModelState.IsValid)
            {
                db.AdjustmentDetails.Add(adjustmentDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemNo = new SelectList(db.Inventories, "ItemNo", "Description", adjustmentDetail.ItemNo);
            return View(adjustmentDetail);
        }

        // GET: AdjustmentDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdjustmentDetail adjustmentDetail = db.AdjustmentDetails.Find(id);
            if (adjustmentDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemNo = new SelectList(db.Inventories, "ItemNo", "Description", adjustmentDetail.ItemNo);
            return View(adjustmentDetail);
        }

        // POST: AdjustmentDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdjustmentDetailId,AdjustmentId,ItemNo,Quantity,Reason")] AdjustmentDetail adjustmentDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adjustmentDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemNo = new SelectList(db.Inventories, "ItemNo", "Description", adjustmentDetail.ItemNo);
            return View(adjustmentDetail);
        }

        // GET: AdjustmentDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdjustmentDetail adjustmentDetail = db.AdjustmentDetails.Find(id);
            if (adjustmentDetail == null)
            {
                return HttpNotFound();
            }
            return View(adjustmentDetail);
        }

        // POST: AdjustmentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdjustmentDetail adjustmentDetail = db.AdjustmentDetails.Find(id);
            db.AdjustmentDetails.Remove(adjustmentDetail);
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
