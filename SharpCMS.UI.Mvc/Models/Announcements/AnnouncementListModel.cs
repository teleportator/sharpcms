using System;
using System.Collections.Generic;
using SharpCMS.BusinessLogic.Views;

namespace SharpCMS.UI.Mvc.Models.Announcements
{
	public class AnnouncementListModel
	{
		public Guid Id { get; set; }
		public IEnumerable<AnnouncementView> Announcements { get; set; }
	}
}