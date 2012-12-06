using System.ComponentModel.DataAnnotations;

namespace SharpCMS.Domain
{
	[Table("SharpCMS_Comments")]
	public class Comment : Content
	{
		[Required]
		public string Text { get; set; }
	}
}