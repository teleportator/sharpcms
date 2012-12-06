using System;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Announcements
{
	public class UpdateAnnouncementOperation : Operation<string>
	{
		private readonly string _contact;
		private readonly bool _displayOnMainMenu;
		private readonly bool _displayOnSideMenu;
		private readonly string _editor;
		private readonly DateTime _expiryDate;
		private readonly Guid _id;
		private readonly bool _isActive;
		private readonly string _organizer;
		private readonly int _sortOrder;
		private readonly DateTime _startingDate;
		private readonly string _startingTime;
		private readonly string _summary;
		private readonly string _text;
		private readonly string _title;
		private readonly string _venue;

		public UpdateAnnouncementOperation(string contact, bool displayOnMainMenu, bool displayOnSideMenu, string editor,
		                                   DateTime expiryDate, Guid id, bool isActive, string organizer, int sortOrder,
		                                   DateTime startingDate, string startingTime, string summary, string text,
		                                   string title, string venue)
		{
			_contact = contact;
			_displayOnMainMenu = displayOnMainMenu;
			_displayOnSideMenu = displayOnSideMenu;
			_editor = editor;
			_expiryDate = expiryDate;
			_id = id;
			_isActive = isActive;
			_organizer = organizer;
			_sortOrder = sortOrder;
			_startingDate = startingDate;
			_startingTime = startingTime;
			_summary = summary;
			_text = text;
			_title = title;
			_venue = venue;
		}

		protected override string PerformWithResult()
		{
			Announcement announcement = Repository.Announcements.GetById(_id);

			announcement.Abstract = _summary;
			announcement.IsActive = _isActive;
			announcement.Text = _text;
			announcement.Title = _title;
			announcement.Updated = DateTime.Now;
			announcement.UpdatedBy = _editor;
			announcement.StartingDate = _startingDate;
			announcement.ExpiryDate = _expiryDate;
			announcement.Venue = _venue;
			announcement.StartingTime = _startingTime;
			announcement.Organizer = _organizer;
			announcement.Contact = _contact;
			announcement.DisplayOnMainMenu = _displayOnMainMenu;
			announcement.DisplayOnSideMenu = _displayOnSideMenu;
			announcement.SortOrder = _sortOrder;

			Repository.SaveChanges();
			SiteMapCache.Clear();

			return announcement.Url;
		}
	}
}