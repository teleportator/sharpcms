using System;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Comments
{
	public class AddCommentOperation : Operation
	{
		private readonly string _createdBy;
		private readonly bool _isActive;
		private readonly Guid _parentId;
		private readonly string _text;

		public AddCommentOperation(string createdBy, bool isActive, Guid parentId, string text)
		{
			_createdBy = createdBy;
			_isActive = isActive;
			_parentId = parentId;
			_text = text;
		}

		protected override void Perform()
		{
			Repository.Comments.Add(new Comment
			{
				Created = DateTime.Now,
				CreatedBy = _createdBy,
				Id = Guid.NewGuid(),
				IsActive = _isActive,
				ParentId = _parentId,
				Text = _text,
				Updated = DateTime.Now,
				UpdatedBy = _createdBy
			});
			Repository.SaveChanges();
		}
	}
}