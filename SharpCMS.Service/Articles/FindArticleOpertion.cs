using System;
using SharpCMS.BusinessLogic.Views;
using SharpCMS.BusinessLogic.Views.Mappers;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Articles
{
	public class FindArticleOpertion : Operation<ArticleView>
	{
		private readonly Guid _id;

		public FindArticleOpertion(Guid id)
		{
			_id = id;
		}

		protected override ArticleView PerformWithResult()
		{
			Article article = Repository.Articles.GetById(_id);

			return article != null ? article.ConvertToArticleView() : null;
		}
	}
}