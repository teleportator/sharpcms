using System;

namespace SharpCMS.BusinessLogic.Views
{
	public class CommentView
	{
		public Guid Id { get; set; }
		public Guid ParentId { get; set; }
		public string Text { get; set; }
		public DateTime Created { get; set; }
		public string CreatedBy { get; set; }
		public DateTime Updated { get; set; }
		public string UpdatedBy { get; set; }
		public bool IsActive { get; set; }
	}
}