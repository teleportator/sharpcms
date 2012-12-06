using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SharpCMS.BusinessLogic;
using SharpCMS.BusinessLogic.Announcements;
using SharpCMS.BusinessLogic.Views;
using SharpCMS.UI.Mvc.Models.Announcements;

namespace SharpCMS.UI.Mvc.Controllers
{
	public class AnnouncementController : ControllerBase
	{
		[Authorize(Roles = "Administrators")]
		public ActionResult Create(Guid id)
		{
			SiteMapItem parentSiteNode = GetSiteMapItem(id);
			var model = new AnnouncementCreateModel
			            	{
			            		ParentTitle = parentSiteNode.Title,
			            		ParentUrl = parentSiteNode.Url,
			            		StartingDate = DateTime.Now.Date,
			            		ExpiryDate = DateTime.Now.Date,
			            		SortOrder = 500,
			            		IsActive = true,
			            		Title = "Акция"
			            	};
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Administrators")]
		public ActionResult Create(Guid id, AnnouncementCreateModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			string announcementUrl =
				BusinessShell.RunWithResult(
					() =>
					new AddAnnouncementOperation(model.Contact, User.Identity.Name, model.DisplayOnMainMenu,
					                             model.DisplayOnSideMenu, model.ExpiryDate, model.IsActive,
					                             model.Organizer, id, model.SortOrder, model.StartingDate,
					                             model.StartingTime, model.Abstract, model.Text, model.Title,
					                             "/announcement/display/{0}", model.Venue));
			return Redirect(announcementUrl);
		}

		[Authorize(Roles = "Administrators")]
		public ActionResult Delete(Guid id)
		{
			SiteMapItem parentNode = GetSiteMapItem(id).ParentNode;
			BusinessShell.Run(() => new DeleteAnnouncementOperation(id));

			return Redirect(parentNode.Url);
		}

		public ActionResult Display(Guid id)
		{
			AnnouncementView announcement = GetAnnouncement(id);
			var model = new AnnouncementDisplayModel
			            	{
			            		Id = id,
			            		Contact = announcement.Contact,
			            		Date = announcement.StartingDate == announcement.ExpiryDate
										? announcement.StartingDate.ToString("dd MMMM yyyy")
										: announcement.StartingDate.ToString("dd MMMM yyyy") + " - " + announcement.ExpiryDate.ToString("dd MMMM yyyy"),
			            		Organizer = announcement.Organizer,
			            		Text = announcement.Text,
			            		Time = announcement.StartingTime,
			            		Title = announcement.Title,
			            		Venue = announcement.Venue
			            	};
			return View(model);
		}

		[Authorize(Roles = "Administrators")]
		public ActionResult Edit(Guid id)
		{
			AnnouncementView announcement = GetAnnouncement(id);

			var model = new AnnouncementEditModel
			            	{
			            		Abstract = announcement.Abstract,
			            		Contact = announcement.Contact,
			            		CurrentUrl = announcement.Url,
			            		DisplayOnMainMenu = announcement.DisplayOnMainMenu,
			            		DisplayOnSideMenu = announcement.DisplayOnSideMenu,
			            		ExpiryDate = announcement.ExpiryDate,
			            		IsActive = announcement.IsActive,
			            		Organizer = announcement.Organizer,
			            		SortOrder = announcement.SortOrder,
			            		StartingDate = announcement.StartingDate,
			            		StartingTime = announcement.StartingTime,
			            		Text = announcement.Text,
			            		Title = announcement.Title,
			            		Venue = announcement.Venue
			            	};
			return View(model);
		}

		[HttpPost]
		[Authorize(Roles = "Administrators")]
		public ActionResult Edit(Guid id, AnnouncementEditModel model)
		{
			SiteMapItem currentNode = GetSiteMapItem(id);
			if (!ModelState.IsValid)
			{
				model.CurrentUrl = currentNode.Url;
				return View(model);
			}

			string announcementUrl =
				BusinessShell.RunWithResult(
					() => new UpdateAnnouncementOperation(model.Contact, model.DisplayOnMainMenu,
					                                      model.DisplayOnSideMenu, User.Identity.Name, model.ExpiryDate, id,
					                                      model.IsActive, model.Organizer, model.SortOrder,
					                                      model.StartingDate, model.StartingTime, model.Abstract, model.Text,
					                                      model.Title, model.Venue));
			return Redirect(announcementUrl);
		}

		public ActionResult List(Guid id)
		{
			var model = new AnnouncementListModel
			            	{
			            		Announcements = GetAnnouncementList(id),
			            		Id = id
			            	};
			return View(model);
		}

		private AnnouncementView GetAnnouncement(Guid id)
		{
			return BusinessShell.RunWithResult(() => new FindAnnouncementOperation(id));
		}

		private IEnumerable<AnnouncementView> GetAnnouncementList(Guid id)
		{
			return BusinessShell.RunWithResult(() => new FindAnnouncementCollectionOperation(id, AllowFullAccess));
		}
	}
}