using System;

namespace SharpCMS.BusinessLogic.Announcements
{
	public class GetCommingAnnouncementsAmountOperation : Operation<int>
	{
		private readonly bool _all;
		private readonly Guid _parentId;

		public GetCommingAnnouncementsAmountOperation(bool all, Guid parentId)
		{
			_all = all;
			_parentId = parentId;
		}

		protected override int PerformWithResult()
		{
			DateTime today = DateTime.Now.Date;

			return Repository.Announcements
				.Count(a => a.ParentId == _parentId && a.ExpiryDate >= today && (a.IsActive || _all));
		}
	}
}