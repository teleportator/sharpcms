using System;
using System.Collections.Generic;
using System.Linq;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Views.Mappers
{
	public static class ArticleExtensions
	{
		public static ArticleView ConvertToArticleView(this Article article)
		{
			if (article == null)
				throw new ArgumentNullException();

			return new ArticleView
			       	{
			       		Id = article.Id,
			       		Title = article.Title,
			       		ParentId = article.ParentId,
			       		Abstract = article.Abstract,
			       		Created = article.Created,
			       		CreatedBy = article.CreatedBy,
			       		IsActive = article.IsActive,
			       		Text = article.Text,
			       		Updated = article.Updated,
						UpdatedBy = article.UpdatedBy,
						DisplayOnMainMenu = article.DisplayOnMainMenu,
						DisplayOnSideMenu = article.DisplayOnSideMenu,
						SortOrder = article.SortOrder,
						Url = article.Url
			       	};
		}

		public static IEnumerable<ArticleView> ConvertToArticleViewCollection(this IEnumerable<Article> articles)
		{
			return articles.Select(a => a.ConvertToArticleView());
		}
	}
}