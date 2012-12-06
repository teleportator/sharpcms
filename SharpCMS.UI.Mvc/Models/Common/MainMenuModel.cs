using System.Collections.Generic;
using SharpCMS.BusinessLogic.Views;

namespace SharpCMS.UI.Mvc.Models.Common
{
	public class MainMenuModel
	{
		public IEnumerable<SiteMapItem> SiteNodes { get; set; }
	}
}