using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SharpCMS.BusinessLogic;
using SharpCMS.BusinessLogic.News;
using SharpCMS.BusinessLogic.Views;
using SharpCMS.UI.Mvc.Models.News;

namespace SharpCMS.UI.Mvc.Controllers
{
	public class NewsController : ControllerBase
	{
		[Authorize(Roles = "Administrators")]
		public ActionResult Create(Guid id)
		{
			SiteMapItem parentNode = GetSiteMapItem(id);
			var model = new NewsCreateModel
			            	{
			            		ParentUrl = parentNode.Url,
			            		ParentTitle = parentNode.Title,
			            		Title = "Новость",
			            		PublishDate = DateTime.Now.Date,
			            		IsActive = true,
			            		SortOrder = 500
			            	};
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Administrators")]
		public ActionResult Create(Guid id, NewsCreateModel model)
		{
			SiteMapItem parentNode = GetSiteMapItem(id);
			if (!ModelState.IsValid)
			{
				model.ParentUrl = parentNode.Url;
				model.ParentTitle = parentNode.Title;
				return View(model);
			}

			string newsItemUrl =
				BusinessShell.RunWithResult(
					() =>
					new AddNewsItemOperation(model.Abstract, User.Identity.Name, model.IsActive, id, model.Text, model.Title, model.PublishDate,
					                         model.DisplayOnMainMenu, model.DisplayOnSideMenu, model.SortOrder,
					                         "/news/display/{0}"));

			return Redirect(newsItemUrl);
		}

		[Authorize(Roles = "Administrators")]
		public ActionResult Delete(Guid id)
		{
			SiteMapItem parentNode = GetSiteMapItem(id).ParentNode;
			BusinessShell.Run(() => new DeleteNewsItemOperation(id));

			return Redirect(parentNode.Url);
		}

		public ActionResult Display(Guid id)
		{
			NewsView newsItem = BusinessShell.RunWithResult(() => new FindNewsItemOperation(id));

			var model = new NewsDisplayModel
			            	{
			            		Id = id,
			            		NewsTitle = newsItem.Title,
			            		NewsText = newsItem.Text,
			            		NewsPublishedDate = newsItem.PublishedDate
			            	};

			return View(model);
		}

		[Authorize(Roles = "Administrators")]
		public ActionResult Edit(Guid id)
		{
			NewsView currentNews = BusinessShell.RunWithResult(() => new FindNewsItemOperation(id));

			var model = new NewsEditModel
			            	{
			            		Abstract = currentNews.Abstract,
								CurrentUrl = currentNews.Url,
								DisplayOnMainMenu = currentNews.DisplayOnMainMenu,
								DisplayOnSideMenu = currentNews.DisplayOnSideMenu,
			            		IsActive = currentNews.IsActive,
			            		PublishDate = currentNews.PublishedDate,
								SortOrder = currentNews.SortOrder,
			            		Text = currentNews.Text,
			            		Title = currentNews.Title
			            	};
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Administrators")]
		public ActionResult Edit(Guid id, NewsEditModel model)
		{
			SiteMapItem currentNode = GetSiteMapItem(id);
			if (!ModelState.IsValid)
			{
				model.CurrentUrl = currentNode.Url;
				return View(model);
			}

			var newsItemUrl =
				BusinessShell.RunWithResult(
					() => new UpdateNewsItemOperation(id, model.Abstract, model.IsActive, model.Text, model.Title, User.Identity.Name,
					                                  model.PublishDate, model.DisplayOnMainMenu, model.DisplayOnSideMenu,
					                                  model.SortOrder));

			return Redirect(newsItemUrl);
		}

		public ActionResult List(Guid id)
		{
			var model = new NewsListModel
			            	{
			            		Id = id,
			            		News = GetNewsList(id)
			            	};
			return View(model);
		}

		private IEnumerable<NewsView> GetNewsList(Guid id)
		{
			return BusinessShell.RunWithResult(() => new FindNewsCollectionOperation(AllowFullAccess, id));
		}
	}
}