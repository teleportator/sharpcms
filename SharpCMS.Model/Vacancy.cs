using System.ComponentModel.DataAnnotations;

namespace SharpCMS.Domain
{
	[Table("SharpCMS_Vacancies")]
	public class Vacancy : Page
	{
		[Required]
		[MaxLength(256)]
		public string Employer { get; set; }

		[Required]
		[MaxLength(256)]
		public string Contact { get; set; }

		[Required]
		[MaxLength(1024)]
		public string Responsibilities { get; set; }

		[Required]
		[MaxLength(1024)]
		public string Demands { get; set; }

		[Required]
		[MaxLength(1024)]
		public string Conditions { get; set; }
	}
}