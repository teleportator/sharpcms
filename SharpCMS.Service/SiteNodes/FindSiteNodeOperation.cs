using System;
using System.Linq;
using SharpCMS.BusinessLogic.Views;

namespace SharpCMS.BusinessLogic.SiteNodes
{
	public class FindSiteNodeOperation : Operation<SiteMapItem>
	{
		private readonly Guid _id;

		public FindSiteNodeOperation(Guid id)
		{
			_id = id;
		}

		protected override SiteMapItem PerformWithResult()
		{
			SiteMap siteMap = SiteMapCache.GetSiteMap();
			return FindSiteNode(siteMap.Root, _id);
		}

		private SiteMapItem FindSiteNode(SiteMapItem layerRootNode, Guid nodeId)
		{
			if (layerRootNode.Id == nodeId)
				return layerRootNode;

			return layerRootNode.ChildNodes.Select(n => FindSiteNode(n, nodeId)).FirstOrDefault(n => n != null);
		}
	}
}