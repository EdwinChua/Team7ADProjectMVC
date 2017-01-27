using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Team7ADProjectMVC.Models;
using Team7ADProjectMVC.Models.InventoryAdjustmentService;

namespace Team7ADProjectMVC.Controllers
{
    public class AdjustmentsController : Controller
    {
        private IInventoryAdjustmentService ivadjustsvc;
        private ProjectEntities db = new ProjectEntities();
        public AdjustmentsController()
        {
            ivadjustsvc = new InventoryAdjustmentService();
        }

        // GET: Adjustments
        public ActionResult ViewAdjustment()
        {
            int userid = ((Employee)Session["user"]).EmployeeId;

            string role = ivadjustsvc.findRolebyUserID(userid);
            ViewBag.employee = new SelectList(db.Employees.Where(x => x.DepartmentId == 6), "EmployeeId", "EmployeeName");

            if (role == "Store Supervisor")
            {
                List<SelectListItem> statuslist = new List<SelectListItem>()
                {
                    new SelectListItem {Text ="Pending Approval"},
                    new SelectListItem {Text ="Approved" },
                    new SelectListItem {Text ="Rejected" },
                };

                ViewBag.status = statuslist;
                var adjustmentlist = ivadjustsvc.findSupervisorAdjustmentList();
                TempData["list"] = adjustmentlist;
                return View(adjustmentlist);
            }

            if (role == "Store Manager")
            {
                List<SelectListItem> statuslist = new List<SelectListItem>()
                {
                    new SelectListItem {Text ="Pending Final Approval" },
                    new SelectListItem {Text ="Approved"},
                    new SelectListItem {Text ="Rejected"},
                };

                ViewBag.status = statuslist;
                var adjustmentlist = ivadjustsvc.findManagerAdjustmentList();
                TempData["list"] = adjustmentlist;
                return View(adjustmentlist);
            }



            return View();
        }

        public ActionResult SearchAdjustment(string employee, string status, string date)
        {

            int userid = ((Employee)Session["user"]).EmployeeId;

            string role = ivadjustsvc.findRolebyUserID(userid);
            ViewBag.employee = new SelectList(db.Employees.Where(x => x.DepartmentId == 6), "EmployeeId", "EmployeeName");

            if (role == "Store Supervisor")
            {
                //var adjustmentlist = ivadjustsvc.findSupervisorAdjustmentList();
                var adjustmentlist = (List<Adjustment>)TempData.Peek("list");

                var result = ivadjustsvc.FindAdjustmentBySearch(adjustmentlist, employee, status, date);


                List<SelectListItem> statuslist = new List<SelectListItem>()
                {
                    new SelectListItem {Text ="Pending Approval"},
                    new SelectListItem {Text ="Approved" },
                    new SelectListItem {Text ="Rejected" },
                };

                ViewBag.status = statuslist;
                return View("ViewAdjustment", result);
            }

            if (role == "Store Supervisor")
            {
                //var adjustmentlist = ivadjustsvc.findManagerAdjustmentList();
                var adjustmentlist = (List<Adjustment>)TempData.Peek("list");
                var result = ivadjustsvc.FindAdjustmentBySearch(adjustmentlist, employee, status, date);

                List<SelectListItem> statuslist = new List<SelectListItem>()
                {
                    new SelectListItem {Text ="Pending Final Approval" },
                    new SelectListItem {Text ="Approved"},
                    new SelectListItem {Text ="Rejected"},
                };

                ViewBag.status = statuslist;
                return View("ViewAdjustment", result);
            }

            return View();


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
        public ActionResult ViewAdjustmentDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adjustment adjustment = ivadjustsvc.findAdjustmentByID(id);
            if (adjustment == null)
            {
                return HttpNotFound();
            }
            List<AdjustmentDetail> dtlist = ivadjustsvc.findDetailByAdjustment(adjustment);
            decimal? total = 0;
            foreach (var item in dtlist)
            {
                var price = item.Quantity * item.Inventory.Price1;

                total += total + price;
            }
            ViewBag.status = ivadjustsvc.findAdjustmentStatus(id);
            ViewBag.sum = total ;
            return View(dtlist);
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
        public ActionResult Create([Bind] adjustment adjust)
        {
            if (ModelState.IsValid)
            {

                Adjustment Adjustment = new Adjustment


                {
                    AdjustmentDate = adjust.AdjustmentDate,
                    EmployeeId = adjust.EmployeeId,
                    Status = adjust.Status
                };
                db.Adjustments.Add(Adjustment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Item = new SelectList(db.Inventories, "ItemNo", "Description");
            return View(adjust);
        }
        //var errors = ModelState.Values.SelectMany(v => v.Errors);
        //if (ModelState.IsValid)
        //{


        //    Adjustment Adjustment = new Adjustment
        //    {
        //        AdjustmentDate = adjust.AdjustmentDate,
        //        EmployeeId = adjust.EmployeeId,
        //        Status = adjust.Status
        //    };


        //    int adid = db.Adjustments.Add(Adjustment).AdjustmentId;

        //    foreach (var item in adjust.AdjustmentDetails)
        //    {
        //        ViewBag.Item = new SelectList(db.Inventories, "ItemNo", "Description", item.ItemNo);
        //        AdjustmentDetail adjustmentdetail = new AdjustmentDetail()
        //        {
        //            AdjustmentId = adid,
        //            ItemNo = item.ItemNo,
        //            Quantity = item.Quantity,
        //            Reason = item.Reason

        //        };
        //        db.AdjustmentDetails.Add(adjustmentdetail);
        //    }
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        //foreach (var item in adjust.AdjustmentDetails)
        //{
        //    ViewBag.Item = new SelectList(db.Inventories, "ItemNo", "Description", item.ItemNo);
        //}

        //return View(adjust);}


        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult AddDetail()
        {
            adjustment adjust = new adjustment();
            var adjustdetail = new adjustmentdetail();
            ViewBag.Item = new SelectList(db.Inventories, "ItemNo", "Description");
            adjust.AdjustmentDetails.Add(adjustdetail);


            return View(adjust);
        }

    }
}

