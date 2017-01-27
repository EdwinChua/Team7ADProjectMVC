using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections;
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

        //[AuthorisePermissions(Permission="ChangeCollectionPoint")]
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

            DataSet1TableAdapters.disbAnalysisTableAdapter da = new DataSet1TableAdapters.disbAnalysisTableAdapter();
            DataSet1.disbAnalysisDataTable dt = new DataSet1.disbAnalysisDataTable();
            da.Fill(dt);
            String categorySelected = Request.Form["Categories"];
            List<DataSet1.disbAnalysisRow> filteredList = dt.Where(x => x.CategoryName == categorySelected&& depts.Contains(x.DepartmentName)).ToList<DataSet1.disbAnalysisRow>();
            DataSet1.disbAnalysisDataTable filteredDT = new DataSet1.disbAnalysisDataTable();
            foreach(DataSet1.disbAnalysisRow r in filteredList)
            {
                filteredDT.ImportRow(r);
            }

            EnumerableRowCollection < Team7ADProjectMVC.DataSet1.disbAnalysisRow > query;
            if (Request.Form["Month2"].Length > 0 && Request.Form["Year2"].Length > 0 && Request.Form["Month3"].Length > 0 && Request.Form["Year3"].Length > 0)
            {
                query = from row in filteredDT.AsEnumerable()
                        where row.Field<DateTime>("DeliveryDate").Month == Int32.Parse(Request.Form["Month"]) && row.Field<DateTime>("DeliveryDate").Year == Int32.Parse(Request.Form["Year"])
                        || row.Field<DateTime>("DeliveryDate").Month == Int32.Parse(Request.Form["Month2"]) && row.Field<DateTime>("DeliveryDate").Year == Int32.Parse(Request.Form["Year2"])
                        || row.Field<DateTime>("DeliveryDate").Month == Int32.Parse(Request.Form["Month3"]) && row.Field<DateTime>("DeliveryDate").Year == Int32.Parse(Request.Form["Year3"])
                        select row;
            }
            else if (Request.Form["Month2"].Length > 0 && Request.Form["Year2"].Length > 0)
            {
                query = from row in filteredDT.AsEnumerable()
                        where row.Field<DateTime>("DeliveryDate").Month == Int32.Parse(Request.Form["Month"]) && row.Field<DateTime>("DeliveryDate").Year == Int32.Parse(Request.Form["Year"])
                        || row.Field<DateTime>("DeliveryDate").Month == Int32.Parse(Request.Form["Month2"]) && row.Field<DateTime>("DeliveryDate").Year == Int32.Parse(Request.Form["Year2"])
                        select row;
            }
            else
            {
                query = from row in filteredDT.AsEnumerable()
                        where row.Field<DateTime>("DeliveryDate").Month == Int32.Parse(Request.Form["Month"]) && row.Field<DateTime>("DeliveryDate").Year == Int32.Parse(Request.Form["Year"])
                        select row;
            }
            ReportDocument cr = new ReportDocument();
            cr.Load(Server.MapPath("~/Reports/CrystalReport1.rpt"));


            DataView data = query.AsDataView();
            cr.SetDataSource(data);
            Session["cr"] = cr;
            return Redirect("ReportViewer.aspx");

        }

        // GET: Rpt/SupplierItem
        public ActionResult ItemSupplier()
        {
            ViewBag.Categories = db.Categories.ToList();
            ViewBag.Months = rptSvc.GetMonthValues();
            ViewBag.Years = rptSvc.GetYearValues();

            return View("ItemSupplierRpt");
        }

        [HttpPost]
        public ActionResult ItemSupplier(FormCollection f)
        {
            DataSet1.PurchaseAnalysisDataTable dt = new DataSet1.PurchaseAnalysisDataTable();
            DataSet1TableAdapters.PurchaseAnalysisTableAdapter da = new DataSet1TableAdapters.PurchaseAnalysisTableAdapter();
            da.Fill(dt);
            ReportDocument cr = new ReportDocument();
            cr.Load(Server.MapPath("~/Reports/CrystalReport2.rpt"));
            cr.SetDataSource((DataTable)dt);
            Session["cr"] = cr;
            return Redirect("/ReportViewer.aspx");

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
                                                     where l.Contains(row.Field<string>("DepartmentName")) && row.Field<DateTime>("DeliveryDate").Month==6 && row.Field<DateTime>("DeliveryDate").Year==2017
                                                     || row.Field<DateTime>("DeliveryDate").Month == 8 && row.Field<DateTime>("DeliveryDate").Year == 2017
                                                                                        select row;

            DataView view = query.AsDataView();
           // DataView view = new DataView();
            //view.Table = dt;
            //view.RowFilter = "DepartmentName='Registrar Department'OR DepartmentName='Computer Science'";
            //view.RowFilter = "Convert(DeliveryDate, 'System.String') LIKE '6/2017'";
            //return Content("<html>" + ((DateTime)dt.Rows[3]["DeliveryDate"]).ToString()+ "</html>");

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