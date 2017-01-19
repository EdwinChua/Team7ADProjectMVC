using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Models.UtilityService
{
    public class UtilityService
    {
        public DateTime GetDateTimeFromPicker(string date)
        {
            List<String> datesplit = date.Split('/').ToList<String>();
            DateTime selected = new DateTime(Int32.Parse((datesplit[0])), Int32.Parse((datesplit[1])), Int32.Parse((datesplit[2])));
            return selected;
        }
    }
}