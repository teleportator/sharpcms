using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpCMS.Domain
{
    [Table("SharpCMS_Comments")]
    public class Comment : Content
    {
        [Required]
        public string Text { get; set; }
    }
}