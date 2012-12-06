using System;
using System.Collections.Generic;
using System.Linq;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Views.Mappers
{
	public static class CommentExtensions
	{
		public static CommentView ConvertToCommentView(this Comment comment)
		{
			if (comment == null)
				throw new ArgumentNullException();

			return new CommentView
			       	{
			       		Id = comment.Id,
			       		ParentId = comment.ParentId,
			       		Created = comment.Created,
			       		CreatedBy = comment.CreatedBy,
			       		Updated = comment.Updated,
			       		UpdatedBy = comment.UpdatedBy,
			       		IsActive = comment.IsActive,
			       		Text = comment.Text
			       	};
		}

		public static IEnumerable<CommentView> ConvertToCommentViewCollection(this IEnumerable<Comment> comments)
		{
			return comments.Select(c => c.ConvertToCommentView()).ToList();
		}
	}
}