using System;

namespace SharpCMS.BusinessLogic.Views
{
	public class ContentItemView
	{
		public Guid Id { get; set; }
		public Guid? ParentId { get; set; }
		public string Title { get; set; }
		public string Abstract { get; set; }
		public string Text { get; set; }
		public DateTime Created { get; set; }
		public string CreatedBy { get; set; }
		public DateTime Updated { get; set; }
		public string UpdatedBy { get; set; }
		public bool IsActive { get; set; }

		public bool DisplayOnMainMenu { get; set; }
		public bool DisplayOnSideMenu { get; set; }
		public int SortOrder { get; set; }
		public string Url { get; set; }
	}
}