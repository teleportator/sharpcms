using System;
using System.Web.Mvc;
using System.Web.Routing;
using SharpCMS.BusinessLogic;
using SharpCMS.BusinessLogic.Comments;
using SharpCMS.UI.Mvc.Infrastructure;
using SharpCMS.UI.Mvc.Models.Comments;

namespace SharpCMS.UI.Mvc.Controllers
{
    public class CommentController : ControllerBase
    {
        public ActionResult Create(Guid id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Guid id, CommentCreateModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
				BusinessShell.Run(() => new AddCommentOperation(User.Identity.Name, true, id, InputDataUtilities.TextAreaHtmlEncode(model.CommentText)));
            }

            return RedirectToAction("List", new RouteValueDictionary { { "id", id } });
        }

        public ActionResult List(Guid id)
        {
        	var model =
        		new CommentsListModel
        			{
        				Comments = BusinessShell.RunWithResult(() => new FindCommentsOperation(AllowFullAccess, id))
        			};

        	return View(model);
        }

        [Authorize]
        public ActionResult Remove(Guid id, Guid contentItemId)
        {
            if (AllowFullAccess)
				BusinessShell.Run(() => new DeleteCommentOperation(id));

            return RedirectToAction("List", new RouteValueDictionary {{"id", contentItemId}});
        }
    }
}