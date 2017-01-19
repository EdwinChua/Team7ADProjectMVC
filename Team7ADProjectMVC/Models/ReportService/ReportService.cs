using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Models.ReportService
{
    public class ReportService : IReportService
    {
        public List<String> GetYearValues()
        {
            List<String> yrs = new List<String>();
            int y = DateTime.Now.Year;
            for (int i = y; i > y - 8; i--)
            {
                yrs.Add(i.ToString());
            }
            return yrs;
        }

        public List<String> GetMonthValues()
        {
            List<String> mths = new List<String>();
            for (int i = 1; i < 13; i++)
            {
                mths.Add(i.ToString());
            }
            return mths;
        }

    }
}