using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpCMS.Domain
{
	[Table("SharpCMS_Companies")]
	public class Company : Page
	{
		[Required]
		[MaxLength(256)]
		public string Logo { get; set; }

		[Required]
		[MaxLength(256)]
		public string PhoneNumber { get; set; }

		[Required]
		[MaxLength(256)]
		public string Address { get; set; }

		[Required]
		[MaxLength(256)]
		public string Email { get; set; }

		[Required]
		[MaxLength(256)]
		public string Hyperlink { get; set; }
	}
}