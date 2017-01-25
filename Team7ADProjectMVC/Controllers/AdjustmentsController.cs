using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Team7ADProjectMVC.Services;
using Team7ADProjectMVC.Models;

namespace Team7ADProjectMVC.Controllers
{
    public class AdjustmentsController : Controller
    {
        private ProjectEntities db = new ProjectEntities();

        // GET: Adjustments
        public ActionResult Index()
        {
            var adjustments = db.Adjustments.Include(a => a.Employee);
            return View(adjustments.ToList());
        }

        // GET: Adjustments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adjustment adjustment = db.Adjustments.Find(id);
            if (adjustment == null)
            {
                return HttpNotFound();
            }
            return View(adjustment);
        }

        // GET: Adjustments/Create
        //public ActionResult Create()
        //{
        //    ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
        //    return View();
        //}

        //// POST: Adjustments/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "AdjustmentId,AdjustmentDate,EmployeeId,SupervisorId,SupervisorAuthorizedDate,HeadId,HeadAuthorizedDate")] Adjustment adjustment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Adjustments.Add(adjustment);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", adjustment.EmployeeId);
        //    return View(adjustment);
        //}

        // GET: Adjustments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adjustment adjustment = db.Adjustments.Find(id);
            if (adjustment == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", adjustment.EmployeeId);
            return View(adjustment);
        }

        // POST: Adjustments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AdjustmentId,AdjustmentDate,EmployeeId,SupervisorId,SupervisorAuthorizedDate,HeadId,HeadAuthorizedDate")] Adjustment adjustment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adjustment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", adjustment.EmployeeId);
            return View(adjustment);
        }

        // GET: Adjustments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adjustment adjustment = db.Adjustments.Find(id);
            if (adjustment == null)
            {
                return HttpNotFound();
            }
            return View(adjustment);
        }

        // POST: Adjustments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Adjustment adjustment = db.Adjustments.Find(id);
            db.Adjustments.Remove(adjustment);
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
        [HttpGet]
        public ActionResult Create()
        {
            var adjust = new adjustment();
            var adjustdetail = new adjustmentdetail();
            ViewBag.Item = new SelectList(db.Inventories, "ItemNo", "Description");

            adjust.AdjustmentDetails.Add(adjustdetail);


            return View(adjust);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Status,AdjustmentDate,EmployeeId,AdjustmentDetails,")] adjustment adjust)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {

                Adjustment Adjustment = new Adjustment
                {
                    AdjustmentDate = adjust.AdjustmentDate,
                    EmployeeId = adjust.EmployeeId,
                    Status = adjust.Status
                };


                int adid = db.Adjustments.Add(Adjustment).AdjustmentId;

                foreach (var item in adjust.AdjustmentDetails)
                {
                    ViewBag.Item = new SelectList(db.Inventories, "ItemNo", "Description", item.ItemNo);
                    AdjustmentDetail adjustmentdetail = new AdjustmentDetail()
                    {
                        AdjustmentId = adid,
                        ItemNo = item.ItemNo,
                        Quantity = item.Quantity,
                        Reason = item.Reason

                    };
                    db.AdjustmentDetails.Add(adjustmentdetail);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            foreach (var item in adjust.AdjustmentDetails)
            {
                ViewBag.Item = new SelectList(db.Inventories, "ItemNo", "Description", item.ItemNo);
            }

            return View(adjust);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult AddDetail()
        {
            var adjust = new adjustment();
            var adjustdetail = new adjustmentdetail();
            ViewBag.Item = new SelectList(db.Inventories, "ItemNo", "Description");
            adjust.AdjustmentDetails.Add(adjustdetail);


            return View(adjust);
        }
    }
}
