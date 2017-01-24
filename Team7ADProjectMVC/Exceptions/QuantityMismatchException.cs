using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Exceptions
{
    public class DisbursementMismatchException : Exception
    {
        public DisbursementMismatchException()
        {
        }

        public DisbursementMismatchException(string message)
        : base(message)
        {
        }
    }
}