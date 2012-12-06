using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SharpCMS.BusinessLogic;
using SharpCMS.BusinessLogic.Ideas;
using SharpCMS.BusinessLogic.Views;
using SharpCMS.UI.Mvc.Infrastructure;
using SharpCMS.UI.Mvc.Models.Ideas;

namespace SharpCMS.UI.Mvc.Controllers
{
	public class IdeaController : ControllerBase
	{
		private readonly Dictionary<int, string> _categories = new Dictionary<int, string>
		                                                       	{
		                                                       		{0, "Детский дом"},
		                                                       		{1, "Инвалиды"},
		                                                       		{2, "Пожилые люди"},
		                                                       		{3, "Пропаганда ЗОЖ"},
		                                                       		{4, "Творчество"},
		                                                       		{5, "Экология"},
		                                                       		{6, "Другое"}
		                                                       	};

		[Authorize(Roles = "Administrators")]
		public ActionResult Create(Guid id)
		{
			SiteMapItem parentNode = GetSiteMapItem(id);
			var model = new IdeaCreateModel
			            	{
			            		Categories = _categories,
			            		ParentId = id,
			            		ParentUrl = parentNode.Url,
			            		ParentTitle = parentNode.Title,
			            		Title = "Идея",
			            		IsActive = true,
			            		SortOrder = 500
			            	};
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Administrators")]
		public ActionResult Create(Guid id, IdeaCreateModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Categories = _categories;
				return View(model);
			}

			string url = CreateIdea(id, model.Title, model.Abstract, model.Text, User.Identity.Name, model.IsActive,
			                        _categories[model.CategoryId], 0, model.SortOrder, model.DisplayOnMainMenu,
			                        model.DisplayOnSideMenu);

			return Redirect(url);
		}

		[Authorize(Roles = "Administrators")]
		public ActionResult Delete(Guid id)
		{
			SiteMapItem parentNode = GetSiteMapItem(id).ParentNode;
			BusinessShell.Run(() => new DeleteIdeaOperation(id));

			return Redirect(parentNode.Url);
		}

		public ActionResult Display(Guid id)
		{
			IdeaView idea = GetIdea(id);
			var model = new IdeaDisplayModel
			            	{
			            		Id = id,
			            		Created = idea.Created,
			            		CreatedBy = idea.CreatedBy,
			            		Text = idea.Text,
			            		Title = idea.Title
			            	};
			return View(model);
		}

		[Authorize(Roles = "Administrators")]
		public ActionResult Edit(Guid id)
		{
			IdeaView idea = GetIdea(id);
			var model = new IdeaEditModel
			            	{
			            		Abstract = idea.Abstract,
			            		Categories = _categories,
			            		CategoryId = _categories.FirstOrDefault(c => c.Value == idea.Category).Key,
			            		CurrentUrl = idea.Url,
			            		DisplayOnMainMenu = idea.DisplayOnMainMenu,
			            		DisplayOnSideMenu = idea.DisplayOnSideMenu,
			            		IsActive = idea.IsActive,
			            		SortOrder = idea.SortOrder,
			            		Text = idea.Text,
			            		Title = idea.Title
			            	};
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Administrators")]
		public ActionResult Edit(Guid id, IdeaEditModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Categories = _categories;
				return View(model);
			}

			string ideaUrl =
				BusinessShell.RunWithResult(
					() => new UpdateIdeaOperation(id, model.Abstract, model.IsActive, model.Text, model.Title, User.Identity.Name,
					                              _categories[model.CategoryId], model.DisplayOnMainMenu, model.DisplayOnSideMenu,
					                              model.SortOrder));

			return Redirect(ideaUrl);
		}

		public ActionResult List(Guid id)
		{
			var model = new IdeaListModel
			            	{
			            		Id = id,
			            		Ideas = GetIdeaList(id)
			            	};
			return View(model);
		}

		public ActionResult Propose(Guid id)
		{
			var model = new IdeaProposeModel
			            	{
			            		Categories = _categories
			            	};
			return View(model);
		}

		[HttpPost]
		public ActionResult Propose(Guid id, IdeaProposeModel model)
		{
			if (User.Identity.IsAuthenticated)
			{
				if (!ModelState.IsValid)
				{
					model.Categories = _categories;
					return View(model);
				}

				CreateIdea(id, model.Title, model.Abstract, InputDataUtilities.TextAreaHtmlEncode(model.Text), User.Identity.Name,
				           false, _categories[model.CategoryId], 0, 500, false, false);

				var ideaCreatedModel = new IdeaSuccessfullyCreatedModel {ParentId = id};
				return View("IdeaCreated", ideaCreatedModel);
			}

			model.Categories = _categories;
			return View(model);
		}

		private string CreateIdea(Guid parentId, string title, string summary, string text, string editor, bool isActive,
		                          string category, int rating,
		                          int sortOrder, bool displayOnMainMenu, bool displayOnSideMenu)
		{
			return BusinessShell.RunWithResult(
				() => new AddIdeaOperation(summary, editor, isActive, parentId, text, title,
				                           category, rating, displayOnMainMenu, displayOnSideMenu,
				                           sortOrder, "/idea/display/{0}"));
		}

		private IdeaView GetIdea(Guid id)
		{
			return BusinessShell.RunWithResult(() => new FindIdeaOperation(id));
		}

		private IEnumerable<IdeaView> GetIdeaList(Guid id)
		{
			return BusinessShell.RunWithResult(() => new FindIdeaCollectionOperation(AllowFullAccess, id));
		}
	}
}