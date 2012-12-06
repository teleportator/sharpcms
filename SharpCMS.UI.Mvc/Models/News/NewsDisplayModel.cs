using System;

namespace SharpCMS.UI.Mvc.Models.News
{
	public class NewsDisplayModel
	{
	    public Guid Id { get; set; }
	    public string NewsTitle { get; set; }
	    public DateTime NewsPublishedDate { get; set; }
	    public string NewsText { get; set; }
	}
}