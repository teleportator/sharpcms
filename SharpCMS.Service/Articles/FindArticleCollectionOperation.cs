using System;
using System.Collections.Generic;
using System.Linq;
using SharpCMS.BusinessLogic.Views;
using SharpCMS.BusinessLogic.Views.Mappers;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Articles
{
	public class FindArticleCollectionOperation : Operation<IEnumerable<ArticleView>>
	{
		private readonly bool _showInactive;
		private readonly Guid? _parentId;

		public FindArticleCollectionOperation(bool showInactive, Guid? parentId)
		{
			_showInactive = showInactive;
			_parentId = parentId;
		}

		protected override IEnumerable<ArticleView> PerformWithResult()
		{
			IEnumerable<Article> articles =
                _showInactive
                    ? Repository.Articles.Find(a => a.ParentId == _parentId).ToList()
                    : Repository.Articles.Find(a => (a.ParentId == _parentId) && a.IsActive).ToList();

			return articles.OrderBy(n => n.Title).ConvertToArticleViewCollection();
		}
	}
}