using System;
using System.ComponentModel.DataAnnotations;

namespace SharpCMS.Domain
{
	[Table("SharpCMS_Contents")]
    public class Content : IEntityWithKey<Guid>
    {
    	public DateTime Created { get; set; }

    	[Required]
    	[MaxLength(256)]
    	public string CreatedBy { get; set; }

    	public Guid Id { get; set; }
    	public bool IsActive { get; set; }

		[ForeignKey("Parent")]
    	public Guid ParentId { get; set; }
    	public DateTime Updated { get; set; }

    	[Required]
    	[MaxLength(256)]
    	public string UpdatedBy { get; set; }
		public Page Parent { get; set; }
    }
}
