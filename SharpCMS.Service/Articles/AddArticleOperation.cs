using System;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Articles
{
	public class AddArticleOperation : Operation<string>
	{
		private readonly string _createdBy;
		private readonly bool _displayOnMainMenu;
		private readonly bool _displayOnSideMenu;
		private readonly bool _isActive;
		private readonly Guid _parentId;
		private readonly int _sortOrder;
		private readonly string _summary;
		private readonly string _text;
		private readonly string _title;
		private readonly string _urlPattern;

		public AddArticleOperation(string summary, string createdBy, bool isActive,
		                           Guid parentId, string text, string title, bool displayOnMainMenu,
		                           bool displayOnSideMenu, int sortOrder, string urlPattern)
		{
			_summary = summary;
			_createdBy = createdBy;
			_isActive = isActive;
			_parentId = parentId;
			_text = text;
			_title = title;
			_displayOnMainMenu = displayOnMainMenu;
			_displayOnSideMenu = displayOnSideMenu;
			_sortOrder = sortOrder;
			_urlPattern = urlPattern;
		}

		protected override string PerformWithResult()
		{
			Guid announcementId = Guid.NewGuid();
			var article = new Article
			              	{
			              		Abstract = _summary,
			              		Created = DateTime.Now,
			              		CreatedBy = _createdBy,
			              		Id = announcementId,
			              		IsActive = _isActive,
			              		ParentId = _parentId,
			              		Text = _text,
			              		Title = _title,
			              		Updated = DateTime.Now,
								UpdatedBy = _createdBy,
								DisplayOnMainMenu = _displayOnMainMenu,
								DisplayOnSideMenu = _displayOnSideMenu,
								SortOrder = _sortOrder,
								Url = String.Format(_urlPattern, announcementId)
			              	};
			Repository.Articles.Add(article);

			Repository.SaveChanges();
			SiteMapCache.Clear();

			return article.Url;
		}
	}
}