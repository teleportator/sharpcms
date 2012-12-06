using System;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Announcements
{
	public class DeleteAnnouncementOperation : Operation
	{
		private readonly Guid _id;

		public DeleteAnnouncementOperation(Guid id)
		{
			_id = id;
		}

		protected override void Perform()
		{
			Announcement announcement = Repository.Announcements.GetById(_id);
			Repository.Announcements.Delete(announcement);

			Repository.SaveChanges();
			SiteMapCache.Clear();
		}
	}
}