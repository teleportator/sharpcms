using System;
using System.Collections.Generic;
using System.Linq;
using SharpCMS.BusinessLogic.Views;
using SharpCMS.BusinessLogic.Views.Mappers;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Announcements
{
	public class FindAnnouncementCollectionOperation : Operation<IEnumerable<AnnouncementView>>
	{
		private readonly Guid _parentId;
		private readonly bool _showInactive;

		public FindAnnouncementCollectionOperation(Guid parentId, bool showInactive)
		{
			_parentId = parentId;
			_showInactive = showInactive;
		}

		protected override IEnumerable<AnnouncementView> PerformWithResult()
		{
			List<Announcement> announcements =
				_showInactive
					? Repository.Announcements.Find(a => a.ParentId == _parentId).ToList()
					: Repository.Announcements.Find(a => (a.ParentId == _parentId) && a.IsActive).ToList();

			return announcements.OrderByDescending(n => n.StartingDate).ConvertToAnnouncementViewCollection();
		}
	}
}