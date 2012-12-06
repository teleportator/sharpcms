using System;
using System.Web.Mvc;
using SharpCMS.BusinessLogic;
using SharpCMS.BusinessLogic.SiteNodes;
using SharpCMS.BusinessLogic.Views;

namespace SharpCMS.UI.Mvc.Controllers
{
    public class ControllerBase : Controller
    {
    	protected bool AllowFullAccess { get; private set; }

        protected SiteMapItem GetSiteMapItem(Guid id)
        {
        	return BusinessShell.RunWithResult(() => new FindSiteNodeOperation(id));
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            AllowFullAccess = User.IsInRole("Administrators");
        }
    }
}