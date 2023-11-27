using StockManager.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockManagerPro_Web.Permissions
{
    public class Roles_Permissions : ActionFilterAttribute
    {
        private string role;

        public Roles_Permissions(string role)
        {
            this.role = role;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["user"] != null)
            {
                Users users = HttpContext.Current.Session["user"] as Users;
                if (users.Role != role)
                {
                    filterContext.Result = new RedirectResult("~/Home/WithoutPermission");
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}