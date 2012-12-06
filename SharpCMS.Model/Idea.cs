using System.ComponentModel.DataAnnotations;

namespace SharpCMS.Domain
{
	[Table("SharpCMS_Ideas")]
	public class Idea : Page
	{
		[Required]
		[MaxLength(256)]
		public string Category { get; set; }

		public int Rating { get; set; }
	}
}