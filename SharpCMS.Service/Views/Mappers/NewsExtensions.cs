using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpCMS.BusinessLogic.Views.Mappers
{
	public static class NewsExtensions
	{
		public static NewsView ConvertToNewsView(this Domain.News news)
		{
			if (news == null)
				throw new ArgumentNullException();

			return new NewsView
			       	{
			       		Id = news.Id,
			       		Title = news.Title,
			       		ParentId = news.ParentId,
			       		Abstract = news.Abstract,
			       		Created = news.Created,
			       		CreatedBy = news.CreatedBy,
			       		IsActive = news.IsActive,
			       		Text = news.Text,
			       		Updated = news.Updated,
			       		UpdatedBy = news.UpdatedBy,
						PublishedDate = news.PublishedDate,
						DisplayOnMainMenu = news.DisplayOnMainMenu,
						DisplayOnSideMenu = news.DisplayOnSideMenu,
						SortOrder = news.SortOrder,
						Url = news.Url
			       	};
		}

		public static IEnumerable<NewsView> ConvertToNewsViewCollection(this IEnumerable<Domain.News> newsList)
		{
			return newsList.Select(n => n.ConvertToNewsView()).ToList();
		}
	}
}