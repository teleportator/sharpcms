using System;
using SharpCMS.BusinessLogic.Views;
using SharpCMS.BusinessLogic.Views.Mappers;

namespace SharpCMS.BusinessLogic.News
{
	public class FindNewsItemOperation : Operation<NewsView>
	{
		private readonly Guid _id;

		public FindNewsItemOperation(Guid id)
		{
			_id = id;
		}

		protected override NewsView PerformWithResult()
		{
			Domain.News newsItem = Repository.News.GetById(_id);

			return newsItem != null ? newsItem.ConvertToNewsView() : null;
		}
	}
}