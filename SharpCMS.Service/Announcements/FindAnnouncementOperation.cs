using System;
using SharpCMS.BusinessLogic.Views;
using SharpCMS.BusinessLogic.Views.Mappers;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Announcements
{
	public class FindAnnouncementOperation : Operation<AnnouncementView>
	{
		private readonly Guid _id;

		public FindAnnouncementOperation(Guid id)
		{
			_id = id;
		}

		protected override AnnouncementView PerformWithResult()
		{
			Announcement announcement = Repository.Announcements.GetById(_id);

			return announcement != null ? announcement.ConvertToAnnouncementView() : null;
		}
	}
}