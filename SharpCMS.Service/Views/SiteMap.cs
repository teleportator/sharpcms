using System;
using System.Collections.Generic;
using System.Linq;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Views
{
	public class SiteMap
	{
		public SiteMap(IEnumerable<Page> pages)
		{
			SiteMapItem root = GetRootNode(pages);
			root.ChildNodes = GetChildsRecursively(pages, root);

			Root = root;
		}

		public SiteMapItem Root { get; set; }

		private IEnumerable<SiteMapItem> GetChildsRecursively(IEnumerable<Page> pages, SiteMapItem parent)
		{
			var childs = new List<SiteMapItem>();
			IEnumerable<Page> childPages = pages.Where(p => p.ParentId == parent.Id)
				.OrderBy(p => p.Title).OrderBy(p => p.SortOrder);
			foreach (Page childPage in childPages)
			{
				SiteMapItem child = ConvertToSiteMapItem(childPage, parent);
				child.ChildNodes = GetChildsRecursively(pages, child);
				childs.Add(child);
			}
			return childs;
		}

		private SiteMapItem ConvertToSiteMapItem(Page page, SiteMapItem parent)
		{
			if (page == null)
				throw new ArgumentNullException();

			var siteMapItem = new SiteMapItem
			{
				DisplayOnMainMenu = page.DisplayOnMainMenu,
				DisplayOnSideMenu = page.DisplayOnSideMenu,
				Id = page.Id,
				ParentNode = parent,
				SortOrder = page.SortOrder,
				Title = page.Title,
				Url = page.Url,
				IsActive = page.IsActive
			};
			return siteMapItem;
		}

		private SiteMapItem GetRootNode(IEnumerable<Page> pages)
		{
			Page rootPage = pages.First(n => n.ParentId == null);
			return ConvertToSiteMapItem(rootPage, null);
		}
	}
}