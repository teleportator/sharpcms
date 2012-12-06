using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;
using SharpCMS.BusinessLogic;
using SharpCMS.BusinessLogic.Companies;
using SharpCMS.BusinessLogic.Views;
using SharpCMS.UI.Mvc.Infrastructure;
using SharpCMS.UI.Mvc.Infrastructure.Abstract;
using SharpCMS.UI.Mvc.Models.Companies;

namespace SharpCMS.UI.Mvc.Controllers
{
	public class CompanyController : ControllerBase
	{
		private readonly ILogoManager _logoManager;

		public CompanyController()
		{
			_logoManager = new LogoManager();
		}

		[Authorize(Roles = "Administrators")]
		public ActionResult Create(Guid id)
		{
			SiteMapItem parentSiteNode = GetSiteMapItem(id);
			var model = new CompanyCreateModel
			            	{
			            		DisplayOnSideMenu = true,
			            		IsActive = true,
			            		LogoUrl = _logoManager.GetPath(String.Empty),
			            		ParentTitle = parentSiteNode.Title,
			            		ParentUrl = parentSiteNode.Url,
			            		SortOrder = 500,
			            		Title = "Новая организация"
			            	};
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Administrators")]
		public ActionResult Create(Guid id, CompanyCreateModel model)
		{
			SiteMapItem parentNode = GetSiteMapItem(id);

			if (!ModelState.IsValid)
			{
				model.ParentTitle = parentNode.Title;
				model.ParentUrl = parentNode.Url;
				return View(model);
			}

			string companyUrl =
				BusinessShell.RunWithResult(
					() =>
					new AddCompanyOperation(model.Abstract, User.Identity.Name, model.IsActive, id, model.Text ?? String.Empty,
					                        model.Title,
					                        _logoManager.GetFileName(model.LogoUrl),
					                        model.PhoneNumber ?? string.Empty, model.Address ?? String.Empty,
					                        model.Email ?? String.Empty, model.Hyperlink ?? String.Empty,
					                        model.DisplayOnMainMenu, model.DisplayOnSideMenu, model.SortOrder,
					                        "/company/display/{0}"));
			return Redirect(companyUrl);
		}

		[Authorize(Roles = "Administrators")]
		public ActionResult Delete(Guid id)
		{
			SiteMapItem parentNode = GetSiteMapItem(id).ParentNode;
			BusinessShell.Run(() => new DeleteCompanyOperation(id));

			return Redirect(parentNode.Url);
		}

		public ActionResult Display(Guid id)
		{
			CompanyView company = GetCompany(id);
			var model = new CompanyDisplayModel
			            	{
			            		Id = id,
			            		Address = company.Address,
			            		Email = company.Email,
			            		Hyperlink = company.Hyperlink,
			            		LogoUrl = _logoManager.GetPath(company.Logo),
			            		Phone = company.PhoneNumber,
			            		Text = company.Text,
			            		Title = company.Title
			            	};
			return View(model);
		}

		[Authorize(Roles = "Administrators")]
		public ActionResult Edit(Guid id)
		{
			CompanyView company = GetCompany(id);
			var model = new CompanyEditModel
			            	{
			            		Abstract = company.Abstract,
			            		CurrentUrl = company.Url,
			            		IsActive = company.IsActive,
			            		Text = company.Text,
			            		Title = company.Title,
			            		Address = company.Address,
			            		Email = company.Email,
			            		Hyperlink = company.Hyperlink,
			            		LogoUrl = _logoManager.GetPath(company.Logo),
			            		PhoneNumber = company.PhoneNumber,
			            		DisplayOnMainMenu = company.DisplayOnMainMenu,
			            		DisplayOnSideMenu = company.DisplayOnSideMenu,
			            		SortOrder = company.SortOrder
			            	};
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Administrators")]
		public ActionResult Edit(Guid id, CompanyEditModel model)
		{
			SiteMapItem currentNode = GetSiteMapItem(id);

			if (!ModelState.IsValid)
			{
				model.CurrentUrl = currentNode.Url;
				return View(model);
			}

			string companyUrl =
				BusinessShell.RunWithResult(
					() => new UpdateCompanyOperation(id, model.Abstract, model.IsActive, model.Text ?? String.Empty,
					                                 model.Title, User.Identity.Name, _logoManager.GetFileName(model.LogoUrl),
					                                 model.PhoneNumber ?? String.Empty, model.Address ?? String.Empty,
					                                 model.Email ?? String.Empty,
					                                 model.Hyperlink ?? String.Empty, model.DisplayOnMainMenu, model.DisplayOnSideMenu,
					                                 model.SortOrder));

			return Redirect(companyUrl);
		}

		public ActionResult List(Guid id)
		{
			var model = new CompanyListModel
			            	{
			            		Id = id,
			            		Companies = GetCompanyList(id)
			            	};
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Administrators")]
		public ActionResult Upload(string qqfile)
		{
			try
			{
				var stream = Request.InputStream;
				if (Request.Files.Count > 0)
				{
					HttpPostedFileBase postedFile = Request.Files[0];
					stream = postedFile.InputStream;
				}

				string fileUrl = _logoManager.GetPath("img" + DateTime.Now.Ticks + ".jpg");

				var image = new Bitmap(stream);
				image = image.Resize(150, 1024);
				image.SaveJpeg(Server.MapPath(fileUrl), 90);

				return Json(new { imageUrl = fileUrl });
			}
			catch (Exception ex)
			{
				return Json(new { imageUrl = _logoManager.GetPath(String.Empty) });
			}
		}

		private CompanyView GetCompany(Guid id)
		{
			return BusinessShell.RunWithResult(() => new FindCompanyOperation(id));
		}

		private IEnumerable<CompanyView> GetCompanyList(Guid id)
		{
			return BusinessShell.RunWithResult(() => new FindCompanyCollectionOperation(AllowFullAccess, id));
		}
	}
}