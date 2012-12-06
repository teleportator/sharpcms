using System.ComponentModel.DataAnnotations;

namespace SharpCMS.Domain
{
    [Table("SharpCMS_Images")]
	public class Image : Content
	{
        [Required]
        [MaxLength(256)]
        public string Source { get; set; }
	}
}