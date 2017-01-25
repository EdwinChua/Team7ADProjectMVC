using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Team7ADProjectMVC.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorisePermissions : AuthorizeAttribute
    {
        public string Permission { get; set; }

        //Called when access is denied
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            //User isn't logged in
            filterContext.Result = new RedirectResult("notallowed");
        }

        //Core authentication, called before each action
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session["User"] != null)
            {
                Employee e = (Employee)httpContext.Session["User"];
                bool authorised = (bool)e.Permission.GetType().GetProperty(this.Permission).GetValue(e.Permission);
                return authorised;
            }
            return false;
        }

    }
}