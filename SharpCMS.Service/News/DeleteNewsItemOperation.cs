using System;

namespace SharpCMS.BusinessLogic.News
{
	public class DeleteNewsItemOperation : Operation
	{
		private readonly Guid _id;

		public DeleteNewsItemOperation(Guid id)
		{
			_id = id;
		}

		protected override void Perform()
		{
			Domain.News news = Repository.News.GetById(_id);
			Repository.News.Delete(news);

			Repository.SaveChanges();
			SiteMapCache.Clear();
		}
	}
}