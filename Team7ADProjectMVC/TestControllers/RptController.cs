using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Team7ADProjectMVC.TestControllers
{
    public class RptController : Controller
    {
        // GET: Rpt
        public ActionResult Index()
        {
            ReportDocument cr = new ReportDocument();
            cr.Load(Server.MapPath("~/Reports/CrystalReport2.rpt"));
            DataSet1TableAdapters.disbAnalysisTableAdapter da = new DataSet1TableAdapters.disbAnalysisTableAdapter();
            DataSet1.disbAnalysisDataTable dt = new DataSet1.disbAnalysisDataTable();
            da.Fill(dt);
            cr.SetDataSource((System.Data.DataTable)dt);
            Stream s = cr.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "application/pdf");
        }
    }
}