using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SharpCMS.BusinessLogic;
using SharpCMS.BusinessLogic.Vacancies;
using SharpCMS.BusinessLogic.Views;
using SharpCMS.UI.Mvc.Models.Vacancies;

namespace SharpCMS.UI.Mvc.Controllers
{
	public class VacancyController : ControllerBase
	{
		[Authorize(Roles = "Administrators")]
		public ActionResult Create(Guid id)
		{
			SiteMapItem parentNode = GetSiteMapItem(id);
			var model = new VacancyCreateModel
			            	{
			            		ParentUrl = parentNode.Url,
			            		ParentTitle = parentNode.Title,
			            		Title = "Вакансия",
			            		IsActive = true,
			            		SortOrder = 500
			            	};
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Administrators")]
		public ActionResult Create(Guid id, VacancyCreateModel model)
		{
			SiteMapItem parentNode = GetSiteMapItem(id);
			if (!ModelState.IsValid)
			{
				model.ParentUrl = parentNode.Url;
				model.ParentTitle = parentNode.Title;
				return View(model);
			}

			string vacancyUrl =
				BusinessShell.RunWithResult(
					() => new AddVacancyOperation(model.Abstract, User.Identity.Name,
						                            model.IsActive, id, model.Text, model.Title,
						                            model.Employer, model.Contact, model.Responsibilities, model.Conditions,
						                            model.Demands, model.DisplayOnMainMenu, model.DisplayOnSideMenu, model.SortOrder, "/vacancy/display/{0}"));
			return Redirect(vacancyUrl);
		}

		[Authorize(Roles = "Administrators")]
		public ActionResult Delete(Guid id)
		{
			SiteMapItem parentNode = GetSiteMapItem(id).ParentNode;
			BusinessShell.Run(() => new DeleteVacancyOperation(id));
			return Redirect(parentNode.Url);
		}

		public ActionResult Display(Guid id)
		{
			VacancyView vacancy = GetVacancy(id);
			var model = new VacancyDisplayModel
			            	{
			            		Id = id,
			            		Conditions = GetCollectionFromField(vacancy.Conditions),
			            		Contact = vacancy.Contact,
			            		Demands = GetCollectionFromField(vacancy.Demands),
			            		Employer = vacancy.Employer,
			            		Responsibilities = GetCollectionFromField(vacancy.Responsibilities),
			            		Text = vacancy.Text,
			            		Title = vacancy.Title
			            	};
			return View(model);
		}

		[Authorize(Roles = "Administrators")]
		public ActionResult Edit(Guid id)
		{
			VacancyView vacancy = GetVacancy(id);
			var model = new VacancyEditModel
			            	{
			            		Abstract = vacancy.Abstract,
			            		Conditions = vacancy.Conditions,
			            		Contact = vacancy.Contact,
								CurrentUrl = vacancy.Url,
			            		Demands = vacancy.Demands,
								DisplayOnMainMenu = vacancy.DisplayOnMainMenu,
								DisplayOnSideMenu = vacancy.DisplayOnSideMenu,
			            		Employer = vacancy.Employer,
			            		IsActive = vacancy.IsActive,
			            		Responsibilities = vacancy.Responsibilities,
								SortOrder = vacancy.SortOrder,
			            		Text = vacancy.Text,
			            		Title = vacancy.Title
			            	};
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Administrators")]
		public ActionResult Edit(Guid id, VacancyEditModel model)
		{
			SiteMapItem currentNode = GetSiteMapItem(id);
			if (!ModelState.IsValid)
			{
				model.CurrentUrl = currentNode.Url;
				return View(model);
			}

			string vacancyUrl =
				BusinessShell.RunWithResult(
					() => new UpdateVacancyOperation(id, model.Abstract, model.IsActive, model.Text, model.Title,
						                                User.Identity.Name, model.Employer, model.Contact, model.Responsibilities,
						                                model.Demands, model.Conditions, model.DisplayOnMainMenu,
						                                model.DisplayOnSideMenu, model.SortOrder));
			return Redirect(vacancyUrl);
		}

		public ActionResult List(Guid id)
		{
			var model = new VacancyListModel
			            	{
			            		Id = id,
			            		Vacancies = GetVacancyList(id)
			            	};
			return View(model);
		}

		private IEnumerable<string> GetCollectionFromField(string field)
		{
			return field.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
		}

		private VacancyView GetVacancy(Guid id)
		{
			return BusinessShell.RunWithResult(() => new FindVacancyOperation(id));
		}

		private IEnumerable<VacancyView> GetVacancyList(Guid id)
		{
			return BusinessShell.RunWithResult(() => new FindVacancyCollectionOperation(AllowFullAccess, id));
		}
	}
}