using System.Collections.Generic;
using SharpCMS.BusinessLogic.Views;

namespace SharpCMS.BusinessLogic.SiteNodes
{
	public class FindMainMenuNodesOperation : Operation<IEnumerable<SiteMapItem>>
	{
		private readonly bool _showInactive;

		public FindMainMenuNodesOperation(bool showInactive)
		{
			_showInactive = showInactive;
		}

		protected override IEnumerable<SiteMapItem> PerformWithResult()
		{
			var siteMap = SiteMapCache.GetSiteMap();
			return FindMainMenuNodes(siteMap.Root, _showInactive);
		}

		private IEnumerable<SiteMapItem> FindMainMenuNodes(SiteMapItem node, bool showInactive, bool recursive = false)
		{
			var nodes = new List<SiteMapItem>();

			if (node.DisplayOnMainMenu && (SiteMapItem.IsParentsActive(node) || showInactive))
				nodes.Add(node);

			foreach (SiteMapItem childNode in node.ChildNodes)
			{
				if (recursive)
				{
					nodes.AddRange(FindMainMenuNodes(childNode, showInactive, true));
				}
				else
				{
					if (childNode.DisplayOnMainMenu && (SiteMapItem.IsParentsActive(childNode) || showInactive))
						nodes.Add(childNode);
				}
			}
			return nodes;
		}
	}
}