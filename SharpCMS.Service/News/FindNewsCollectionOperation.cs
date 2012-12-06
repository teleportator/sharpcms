using System;
using System.Collections.Generic;
using System.Linq;
using SharpCMS.BusinessLogic.Views;
using SharpCMS.BusinessLogic.Views.Mappers;

namespace SharpCMS.BusinessLogic.News
{
	public class FindNewsCollectionOperation : Operation<IEnumerable<NewsView>>
	{
		private readonly bool _showAll;
		private readonly Guid _parentId;

		public FindNewsCollectionOperation(bool showAll, Guid parentId)
		{
			_showAll = showAll;
			_parentId = parentId;
		}

		protected override IEnumerable<NewsView> PerformWithResult()
		{
			List<Domain.News> news =
				_showAll
					? Repository.News.Find(n => n.ParentId == _parentId).ToList()
					: Repository.News
					  	.Find(n => (n.PublishedDate < DateTime.Now) && n.IsActive && (n.ParentId == _parentId))
					  	.ToList();

			return news.OrderByDescending(n => n.PublishedDate).ConvertToNewsViewCollection();
		}
	}
}