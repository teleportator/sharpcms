using System;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Announcements
{
	public class AddAnnouncementOperation : Operation<string>
	{
		private readonly string _contact;
		private readonly string _createdBy;
		private readonly bool _displayOnMainMenu;
		private readonly bool _displayOnSideMenu;
		private readonly DateTime _expiryDate;
		private readonly bool _isActive;
		private readonly string _organizer;
		private readonly Guid _parentId;
		private readonly int _sortOrder;
		private readonly DateTime _startingDate;
		private readonly string _startingTime;
		private readonly string _summary;
		private readonly string _text;
		private readonly string _title;
		private readonly string _urlPattern;
		private readonly string _venue;

		public AddAnnouncementOperation(string contact, string createdBy, bool displayOnMainMenu, bool displayOnSideMenu,
		                                DateTime expiryDate, bool isActive, string organizer, Guid parentId,
		                                int sortOrder, DateTime startingDate, string startingTime, string summary, string text,
		                                string title, string urlPattern, string venue)
		{
			_contact = contact;
			_createdBy = createdBy;
			_displayOnMainMenu = displayOnMainMenu;
			_displayOnSideMenu = displayOnSideMenu;
			_expiryDate = expiryDate;
			_isActive = isActive;
			_organizer = organizer;
			_parentId = parentId;
			_sortOrder = sortOrder;
			_startingDate = startingDate;
			_startingTime = startingTime;
			_summary = summary;
			_text = text;
			_title = title;
			_urlPattern = urlPattern;
			_venue = venue;
		}

		protected override string PerformWithResult()
		{
			Guid announcementId = Guid.NewGuid();

			var announcement = new Announcement
			                   	{
			                   		Abstract = _summary,
			                   		Created = DateTime.Now,
			                   		CreatedBy = _createdBy,
			                   		Id = announcementId,
			                   		IsActive = _isActive,
			                   		ParentId = _parentId,
			                   		Text = _text,
			                   		Title = _title,
			                   		Updated = DateTime.Now,
			                   		UpdatedBy = _createdBy,
			                   		StartingDate = _startingDate,
			                   		ExpiryDate = _expiryDate,
			                   		Venue = _venue,
			                   		StartingTime = _startingTime,
			                   		Organizer = _organizer,
			                   		Contact = _contact,
									DisplayOnMainMenu = _displayOnMainMenu,
									DisplayOnSideMenu = _displayOnSideMenu,
									SortOrder = _sortOrder,
									Url = String.Format(_urlPattern, announcementId)
			                   	};
			Repository.Announcements.Add(announcement);

			Repository.SaveChanges();
			SiteMapCache.Clear();

			return announcement.Url;
		}
	}
}