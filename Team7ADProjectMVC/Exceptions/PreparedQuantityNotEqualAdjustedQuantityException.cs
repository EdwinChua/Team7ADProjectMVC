using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Team7ADProjectMVC.Exceptions
{
    public class PreparedQuantityNotEqualAdjustedQuantityException : Exception
    {
        public PreparedQuantityNotEqualAdjustedQuantityException()
        {
        }

        public PreparedQuantityNotEqualAdjustedQuantityException(string message)
        : base(message)
        {
        }
    }
}