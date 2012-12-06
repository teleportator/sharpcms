using System;
using System.Linq;
using System.Web.Mvc;
using SharpCMS.BusinessLogic;
using SharpCMS.BusinessLogic.SiteNodes;
using SharpCMS.BusinessLogic.Views;
using SharpCMS.UI.Mvc.Models.Common;

namespace SharpCMS.UI.Mvc.Controllers
{
	public class CommonController : ControllerBase
	{
        public ActionResult DisplayMainMenu()
		{
			var model = new MainMenuModel
			            	{SiteNodes = BusinessShell.RunWithResult(() => new FindMainMenuNodesOperation(AllowFullAccess))};
			return View("MenuPartial", model);
		}

		public ActionResult DisplaySideMenu(Guid id)
		{
			var model = new SideMenuModel();

			SiteMapItem selectedNode = GetSiteMapItem(id);
			SiteMapItem rootSectionNode = GetRootSectionNode(selectedNode);
			model.RootSectionTitle = rootSectionNode.Title;
			model.RootSectionUrl = rootSectionNode.Url;
			model.Links = GetLinks(selectedNode.Id, selectedNode, Guid.Empty, new SideMenuCollectionModel(), AllowFullAccess);

			return View("SideMenuPartial", model);
		}

		public ActionResult Title(Guid? id)
		{
			var model = new PageTitleModel();
			SiteMapItem selectedNode = null;
			if (id.HasValue)
			{
				selectedNode = GetSiteMapItem((Guid) id);
			}
			model.Title = selectedNode == null
			              	? "Я волонтер Ставрополя"
			              	: selectedNode.Title + " - " + "Я волонтер Ставрополя";
			return View("TitlePartial", model);
		}

		private static SideMenuCollectionModel GetLinks(Guid selectedNodeId, SiteMapItem node, Guid childRootId,
		                                                SideMenuCollectionModel childs, bool displayInactive)
		{
			if (node.Layer > 0)
			{
				var collection =
					new SideMenuCollectionModel(node.ChildNodes
					                            	.Where(n => n.DisplayOnSideMenu && (n.IsActive || displayInactive))
					                            	.Select(n => new SideMenuLinkModel
					                            	             	{
					                            	             		IsCurrent = n.Id == selectedNodeId,
					                            	             		Title = n.Title,
					                            	             		Url = n.Url,
					                            	             		Childs = n.Id == childRootId ? childs : new SideMenuCollectionModel()
					                            	             	}).ToList());
				collection.HasCurrent = node.ChildNodes.Any(n => n.Id == selectedNodeId) || childs.HasCurrent;
				return GetLinks(selectedNodeId, node.ParentNode, node.Id, collection, displayInactive);
			}

			return childs;
		}

		private static SiteMapItem GetRootSectionNode(SiteMapItem node)
		{
			return node.Layer == 1 ? node : GetRootSectionNode(node.ParentNode);
		}
	}
}