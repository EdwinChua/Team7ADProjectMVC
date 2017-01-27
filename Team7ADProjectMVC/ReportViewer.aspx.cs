using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Team7ADProjectMVC
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //DataSet1TableAdapters.disbAnalysisTableAdapter da = new DataSet1TableAdapters.disbAnalysisTableAdapter();
            //DataSet1.disbAnalysisDataTable dt = new DataSet1.disbAnalysisDataTable();
            //da.Fill(dt);
            //DataView data = (DataView)Session["data"];
            //String path = Session["path"].ToString();
            //ReportDocument cr = new ReportDocument();
            //cr.Load(Server.MapPath("~/Reports/CrystalReport1.rpt"));
            //cr.SetDataSource((DataTable)dt);
            CrystalReportViewer1.ReportSource = Session["cr"];
            Session["cr"] = "";
        }
    }
}