using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace ERP.Web
{
    public class AuthLogin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetInt32("userid") == null)
            {
                var values = new RouteValueDictionary(new
                {
                    action = "Login",
                    controller = "Login",
                    area = "Login",
                });
                filterContext.Result = new RedirectToRouteResult(values);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
