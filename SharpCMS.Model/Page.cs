using System;
using System.ComponentModel.DataAnnotations;

namespace SharpCMS.Domain
{
	[Table("SharpCMS_Pages")]
	public abstract class Page : IEntityWithKey<Guid>
	{
		[Required]
		[MaxLength(256)]
		public string Abstract { get; set; }

		public DateTime Created { get; set; }

		[Required]
		[MaxLength(256)]
		public string CreatedBy { get; set; }

		public bool DisplayOnMainMenu { get; set; }
		public bool DisplayOnSideMenu { get; set; }
		public bool IsActive { get; set; }

		[ForeignKey("Parent")]
		public Guid? ParentId { get; set; }

		public Page Parent { get; set; }
		public int SortOrder { get; set; }

		[Required]
		[MaxLength(256)]
		public string Title { get; set; }

		[Required]
		public string Text { get; set; }

		[Required]
		[MaxLength(1024)]
		public string Url { get; set; }

		public DateTime Updated { get; set; }

		[Required]
		[MaxLength(256)]
		public string UpdatedBy { get; set; }

		#region IEntityWithKey<Guid> Members

		public Guid Id { get; set; }

		#endregion
	}
}