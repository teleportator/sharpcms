using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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