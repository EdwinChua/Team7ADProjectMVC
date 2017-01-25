using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Team7ADProjectMVC.Models;
using Team7ADProjectMVC.Models.ReportService;

namespace Team7ADProjectMVC.TestControllers
{
    public class RptController : Controller
    {
        private ProjectEntities db = new ProjectEntities();
        private IReportService rptSvc = new ReportService();
        // GET: Rpt

        [AuthorisePermissions(Permission="MakeRequisition")]
        public ActionResult Index()
        {
            
            ViewBag.Departments = db.Departments.ToList();
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Months = rptSvc.GetMonthValues();
            ViewBag.Years = rptSvc.GetYearValues();

            return View("ItemDeptRpt");
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {

            List<string> depts = Request.Form["Departments"].Split(',').ToList<string>();
            string htmlstuff = "";
            foreach (string pc in depts)
            {
                htmlstuff = htmlstuff + pc + "<br>";
            }

            List<string> cats = Request.Form["Categories"].Split(',').ToList<string>();
            foreach (string pc in cats)
            {
                htmlstuff = htmlstuff + pc + "<br>";
            }

            DataSet1TableAdapters.disbAnalysisTableAdapter da = new DataSet1TableAdapters.disbAnalysisTableAdapter();
            DataSet1.disbAnalysisDataTable dt = new DataSet1.disbAnalysisDataTable();
            da.Fill(dt);

            EnumerableRowCollection<Team7ADProjectMVC.DataSet1.disbAnalysisRow> query;
            if (Request.Form["Month2"].Length > 0 && Request.Form["Year2"].Length > 0 && Request.Form["Month3"].Length > 0 && Request.Form["Year3"].Length > 0)
            {           
                query = from row in dt.AsEnumerable()
                        where depts.Contains(row.Field<string>("DepartmentName"))
                        && row.Field<DateTime>("ApprovedDate").Month == Int32.Parse(Request.Form["Month"]) && row.Field<DateTime>("ApprovedDate").Year == Int32.Parse(Request.Form["Year"])
                        || row.Field<DateTime>("ApprovedDate").Month == Int32.Parse(Request.Form["Month2"]) && row.Field<DateTime>("ApprovedDate").Year == Int32.Parse(Request.Form["Year2"])
                        || row.Field<DateTime>("ApprovedDate").Month == Int32.Parse(Request.Form["Month3"]) && row.Field<DateTime>("ApprovedDate").Year == Int32.Parse(Request.Form["Year3"])
                        && cats.Contains(row.Field<String>("CategoryName"))
                        select row;
            } else if (Request.Form["Month2"].Length > 0 && Request.Form["Year2"].Length > 0)
            {
                query = from row in dt.AsEnumerable()
                        where depts.Contains(row.Field<string>("DepartmentName"))
                        && row.Field<DateTime>("ApprovedDate").Month == Int32.Parse(Request.Form["Month"]) && row.Field<DateTime>("ApprovedDate").Year == Int32.Parse(Request.Form["Year"])
                        || row.Field<DateTime>("ApprovedDate").Month == Int32.Parse(Request.Form["Month2"]) && row.Field<DateTime>("ApprovedDate").Year == Int32.Parse(Request.Form["Year2"])
                        && cats.Contains(row.Field<String>("CategoryName"))
                        select row;
            }
            else
            {
                query = from row in dt.AsEnumerable()
                        where depts.Contains(row.Field<string>("DepartmentName"))
                        && row.Field<DateTime>("ApprovedDate").Month == Int32.Parse(Request.Form["Month"]) && row.Field<DateTime>("ApprovedDate").Year == Int32.Parse(Request.Form["Year"])                                                        
                        && cats.Contains(row.Field<String>("CategoryName"))
                        select row;
            }   
            //return Content("<html>" + htmlstuff + "</html>");
            ReportDocument cr = new ReportDocument();
            cr.Load(Server.MapPath("~/Reports/CrystalReport2.rpt"));
            

            DataView view = query.AsDataView();
            cr.SetDataSource(view);
            Stream s = cr.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");

        }

        // GET: Rpt/SupplierItem
        public ActionResult ItemSupplier()
        {
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Months = rptSvc.GetMonthValues();
            ViewBag.Years = rptSvc.GetYearValues();

            return View("ItemSupplierRpt");
        }

        public ActionResult test()
        {
            ReportDocument cr = new ReportDocument();
            cr.Load(Server.MapPath("~/Reports/CrystalReport1.rpt"));
            DataSet1TableAdapters.disbAnalysisTableAdapter da = new DataSet1TableAdapters.disbAnalysisTableAdapter();
            DataSet1.disbAnalysisDataTable dt = new DataSet1.disbAnalysisDataTable();
            da.Fill(dt);
            string[] depts = { "Registrar Department", "Computer Science" };
            List<String> l = new List<string>();
            l.Add("Registrar Department");
            l.Add("Computer Science");

            EnumerableRowCollection<Team7ADProjectMVC.DataSet1.disbAnalysisRow> query = from row in dt.AsEnumerable()
                                                     where l.Contains(row.Field<string>("DepartmentName")) && row.Field<DateTime>("ApprovedDate").Month==6 && row.Field<DateTime>("ApprovedDate").Year==2017
                                                     || row.Field<DateTime>("ApprovedDate").Month == 8 && row.Field<DateTime>("ApprovedDate").Year == 2017
                                                                                        select row;

            DataView view = query.AsDataView();
           // DataView view = new DataView();
            //view.Table = dt;
            //view.RowFilter = "DepartmentName='Registrar Department'OR DepartmentName='Computer Science'";
            //view.RowFilter = "Convert(ApprovedDate, 'System.String') LIKE '6/2017'";
            //return Content("<html>" + ((DateTime)dt.Rows[3]["ApprovedDate"]).ToString()+ "</html>");

            cr.SetDataSource(view);
            Stream s = cr.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");


        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Index()
        //{
        //    ReportDocument cr = new ReportDocument();
        //    cr.Load(Server.MapPath("~/Reports/CrystalReport2.rpt"));
        //    DataSet1TableAdapters.disbAnalysisTableAdapter da = new DataSet1TableAdapters.disbAnalysisTableAdapter();
        //    DataSet1.disbAnalysisDataTable dt = new DataSet1.disbAnalysisDataTable();
        //    da.Fill(dt);
        //    cr.SetDataSource((System.Data.DataTable)dt);
        //    Stream s = cr.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    return File(s, "application/pdf");
        //}
    }
}