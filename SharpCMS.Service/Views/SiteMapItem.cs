using System;
using System.Collections.Generic;

namespace SharpCMS.BusinessLogic.Views
{
	public class SiteMapItem
	{
		private IEnumerable<SiteMapItem> _childNodes;

		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Url { get; set; }
		public SiteMapItem ParentNode { get; set; }
		public bool DisplayOnMainMenu { get; set; }
		public bool DisplayOnSideMenu { get; set; }
		public bool IsActive { get; set; }
		public int SortOrder { get; set; }

		public IEnumerable<SiteMapItem> ChildNodes
		{
			get { return _childNodes ?? (_childNodes = new List<SiteMapItem>()); }
			set { _childNodes = value; }
		}

		public int Layer
		{
			get { return (ParentNode == null) ? 0 : ParentNode.Layer + 1; }
		}

		public static bool IsParentsActive(SiteMapItem siteNode)
		{
			if (siteNode != null)
			{
				return siteNode.IsActive && IsParentsActive(siteNode.ParentNode);
			}
			return true;
		}
	}
}