using System.Collections.Generic;

namespace Team7ADProjectMVC.Models.ReportService
{
    public interface IReportService
    {
        List<string> GetMonthValues();
        List<string> GetYearValues();
    }
}