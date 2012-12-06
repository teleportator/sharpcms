using System;
using System.Collections.Generic;
using System.Linq;
using SharpCMS.BusinessLogic.Views;
using SharpCMS.BusinessLogic.Views.Mappers;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Comments
{
	public class FindCommentsOperation : Operation<IEnumerable<CommentView>>
	{
		private readonly bool _showInactive;
		private readonly Guid _parentId;

		public FindCommentsOperation(bool showInactive, Guid parentId)
		{
			_showInactive = showInactive;
			_parentId = parentId;
		}

		protected override IEnumerable<CommentView> PerformWithResult()
		{
			List<Comment> comments =
				_showInactive
					? Repository.Comments.Find(c => c.ParentId == _parentId).ToList()
					: Repository.Comments.Find(c => (c.ParentId == _parentId) && c.IsActive).ToList();

			return comments.OrderBy(n => n.Created).ConvertToCommentViewCollection();
		}
	}
}