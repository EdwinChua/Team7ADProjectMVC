﻿using System.Collections.Generic;
using System.Data;

namespace Team7ADProjectMVC.Models.ReportService
{
    public interface IReportService
    {
        List<string> GetMonthValues();
        List<string> GetYearValues();
        List<YrMth> GetListOfYrMthFromUI(string yr1, string mth1, string yr2, string mth2, string yr3, string mth3);
        DataView GetDataForDisbAnalysis(List<YrMth> yrMthList, List<string> depts, string categorySelected);
        DataView GetDataForSupplierAnalysis(List<YrMth> yrMthList, string categorySelected);
    }
}