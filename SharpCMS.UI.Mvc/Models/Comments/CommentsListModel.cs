using System.Collections.Generic;
using SharpCMS.BusinessLogic.Views;

namespace SharpCMS.UI.Mvc.Models.Comments
{
	public class CommentsListModel
	{
		public IEnumerable<CommentView> Comments { get; set; }
	}
}