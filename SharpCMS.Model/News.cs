using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharpCMS.Domain
{
    [Table("SharpCMS_News")]
    public class News : Page
    {
        public DateTime PublishedDate { get; set; }
    }
}