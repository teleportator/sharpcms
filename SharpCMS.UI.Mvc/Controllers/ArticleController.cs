using System;
using System.Web.Mvc;
using SharpCMS.BusinessLogic;
using SharpCMS.BusinessLogic.Articles;
using SharpCMS.BusinessLogic.Views;
using SharpCMS.UI.Mvc.Infrastructure;
using SharpCMS.UI.Mvc.Infrastructure.Abstract;
using SharpCMS.UI.Mvc.Models.Articles;

namespace SharpCMS.UI.Mvc.Controllers
{
	public class ArticleController : ControllerBase
	{
		private readonly IUrlPatternFactory _urlPatternFactory;

		public ArticleController()
		{
			_urlPatternFactory = new UrlPatternFactory();
		}

		public ActionResult Display(Guid id)
		{
			ArticleView article = GetArticle(id);
			var model = new ArticleDisplayModel
			            	{
			            		Title = article.Title,
			            		Text = article.Text
			            	};
			return View(model);
		}

		[Authorize(Roles = "Administrators")]
		public ActionResult Create(Guid id, string type)
		{
			SiteMapItem parentSiteNode = GetSiteMapItem(id);
			var model = new ArticleCreateModel
			            	{
			            		DisplayOnSideMenu = true,
			            		IsActive = true,
			            		ParentTitle = parentSiteNode.Title,
			            		ParentUrl = parentSiteNode.Url,
			            		SortOrder = 500,
			            		Title = "Новая страница"
			            	};
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Administrators")]
		public ActionResult Create(Guid id, string type, ArticleCreateModel model)
		{
			SiteMapItem parentNode = GetSiteMapItem(id);
			model.ParentTitle = parentNode.Title;
			model.ParentUrl = parentNode.Url;

			if (!ModelState.IsValid)
				return View(model);

			string articleUrl =
				BusinessShell.RunWithResult(
					() => new AddArticleOperation(model.Abstract, User.Identity.Name,
					                              model.IsActive, id, model.Text, model.Title,
					                              model.DisplayOnMainMenu, true, model.SortOrder,
					                              _urlPatternFactory.GetUrlPatternFor(type.ToLower())));

			return Redirect(articleUrl);
		}

		[Authorize(Roles = "Administrators")]
		public ActionResult Delete(Guid id)
		{
			SiteMapItem parentNode = GetSiteMapItem(id).ParentNode;
			DeleteArticle(id);
			return Redirect(parentNode.Url);
		}

		[Authorize(Roles = "Administrators")]
		public ActionResult Edit(Guid id, string type)
		{
			ArticleView article = GetArticle(id);
			var model = new ArticleEditModel
			            	{
			            		Abstract = article.Abstract,
								CurrentUrl = article.Url,
								DisplayOnMainMenu = article.DisplayOnMainMenu,
								DisplayOnSideMenu = article.DisplayOnSideMenu,
			            		IsActive = article.IsActive,
								SortOrder = article.SortOrder,
			            		Text = article.Text,
			            		Title = article.Title
			            	};
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Administrators")]
		public ActionResult Edit(Guid id, string type, ArticleEditModel model)
		{
			model.CurrentUrl = GetSiteMapItem(id).Url;
			if (!ModelState.IsValid)
				return View(model);

		    string articleUrl =
		        BusinessShell.RunWithResult(
		            () => new UpdateArticleOperation(id, model.Abstract, model.IsActive, model.Text,
		                                             model.Title, User.Identity.Name, model.DisplayOnMainMenu,
		                                             model.DisplayOnSideMenu, model.SortOrder));
			return Redirect(articleUrl);
		}

		private void DeleteArticle(Guid id)
		{
			BusinessShell.Run(() => new DeleteArticleOperation(id));
		}

		private ArticleView GetArticle(Guid id)
		{
			return BusinessShell.RunWithResult(() => new FindArticleOpertion(id));
		}
	}
}