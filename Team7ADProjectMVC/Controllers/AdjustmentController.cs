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
    public class AdjustmentController : Controller
    {
        private ProjectEntities db = new ProjectEntities();

        // GET: Adjustment
        public ActionResult Index()
        {
            var adjustments = db.Adjustments.Include(a => a.Employee);
            return View(adjustments.ToList());
        }

        // GET: Adjustment/Details/5
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

        // GET: Adjustment/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName");
            return View();
        }

        // POST: Adjustment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdjustmentId,AdjustmentDate,EmployeeId,SupervisorId,SupervisorAuthorizedDate,HeadId,HeadAuthorizedDate")] Adjustment adjustment)
        {
            if (ModelState.IsValid)
            {
                db.Adjustments.Add(adjustment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "EmployeeName", adjustment.EmployeeId);
            return View(adjustment);
        }

        // GET: Adjustment/Edit/5
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

        // POST: Adjustment/Edit/5
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

        // GET: Adjustment/Delete/5
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

        // POST: Adjustment/Delete/5
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
    }
}
