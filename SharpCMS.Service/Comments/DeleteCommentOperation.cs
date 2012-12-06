using System;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Comments
{
	public class DeleteCommentOperation : Operation
	{
		private readonly Guid _id;

		public DeleteCommentOperation(Guid id)
		{
			_id = id;
		}

		protected override void Perform()
		{
			Comment comment = Repository.Comments.GetById(_id);

			Repository.Comments.Delete(comment);
			Repository.SaveChanges();
		}
	}
}