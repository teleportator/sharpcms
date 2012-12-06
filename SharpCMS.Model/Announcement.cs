using System;
using System.ComponentModel.DataAnnotations;

namespace SharpCMS.Domain
{
	[Table("SharpCMS_Announcements")]
	public class Announcement : Page
	{
		public DateTime StartingDate { get; set; }

		public DateTime ExpiryDate { get; set; }

		[Required]
		[MaxLength(256)]
		public string Venue { get; set; }

		[Required]
		[MaxLength(256)]
		public string StartingTime { get; set; }

		[Required]
		[MaxLength(256)]
		public string Organizer { get; set; }

		[Required]
		[MaxLength(256)]
		public string Contact { get; set; }
	}
}