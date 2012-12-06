using System;
using System.Collections.Generic;
using System.Linq;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Views.Mappers
{
	public static class AnnouncementExtensions
	{
		public static AnnouncementView ConvertToAnnouncementView(this Announcement announcement)
		{
			if (announcement == null)
				throw new ArgumentNullException();

			return new AnnouncementView
			       	{
			       		Id = announcement.Id,
			       		Title = announcement.Title,
			       		ParentId = announcement.ParentId,
			       		Abstract = announcement.Abstract,
			       		Created = announcement.Created,
			       		CreatedBy = announcement.CreatedBy,
			       		IsActive = announcement.IsActive,
			       		Text = announcement.Text,
			       		Updated = announcement.Updated,
			       		UpdatedBy = announcement.UpdatedBy,
			       		StartingDate = announcement.StartingDate,
			       		ExpiryDate = announcement.ExpiryDate,
			       		Venue = announcement.Venue,
			       		StartingTime = announcement.StartingTime,
			       		Organizer = announcement.Organizer,
			       		Contact = announcement.Contact,
						DisplayOnMainMenu = announcement.DisplayOnMainMenu,
						DisplayOnSideMenu = announcement.DisplayOnSideMenu,
						SortOrder = announcement.SortOrder,
						Url = announcement.Url
			       	};
		}

		public static IEnumerable<AnnouncementView> ConvertToAnnouncementViewCollection(
			this IEnumerable<Announcement> announcements)
		{
			return announcements.Select(a => a.ConvertToAnnouncementView()).ToList();
		}
	}
}