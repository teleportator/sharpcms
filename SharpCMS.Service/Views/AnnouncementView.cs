using System;

namespace SharpCMS.BusinessLogic.Views
{
	public class AnnouncementView : ContentItemView
	{
		public DateTime StartingDate { get; set; }
		public DateTime ExpiryDate { get; set; }
		public string Venue { get; set; }
		public string StartingTime { get; set; }
		public string Organizer { get; set; }
		public string Contact { get; set; }
	}
}