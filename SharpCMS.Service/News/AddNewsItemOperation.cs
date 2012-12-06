using System;

namespace SharpCMS.BusinessLogic.News
{
	public class AddNewsItemOperation : Operation<string>
	{
		private readonly string _createdBy;
		private readonly bool _displayOnMainMenu;
		private readonly bool _displayOnSideMenu;
		private readonly bool _isActive;
		private readonly Guid _parentId;
		private readonly DateTime _publishedDate;
		private readonly int _sortOrder;
		private readonly string _summary;
		private readonly string _text;
		private readonly string _title;
		private readonly string _urlPattern;

		public AddNewsItemOperation(string summary, string createdBy, bool isActive,
		                            Guid parentId, string text, string title, DateTime publishedDate, bool displayOnMainMenu,
		                            bool displayOnSideMenu, int sortOrder, string urlPattern)
		{
			_summary = summary;
			_createdBy = createdBy;
			_isActive = isActive;
			_parentId = parentId;
			_text = text;
			_title = title;
			_publishedDate = publishedDate;
			_displayOnMainMenu = displayOnMainMenu;
			_displayOnSideMenu = displayOnSideMenu;
			_sortOrder = sortOrder;
			_urlPattern = urlPattern;
		}

		protected override string PerformWithResult()
		{
			Guid newsItemId = Guid.NewGuid();
			var newsItem = new Domain.News
			               	{
			               		Abstract = _summary,
			               		Created = DateTime.Now,
			               		CreatedBy = _createdBy,
			               		Id = newsItemId,
			               		IsActive = _isActive,
			               		ParentId = _parentId,
			               		Text = _text,
			               		Title = _title,
			               		Updated = DateTime.Now,
			               		UpdatedBy = _createdBy,
			               		PublishedDate = _publishedDate,
			               		DisplayOnMainMenu = _displayOnMainMenu,
			               		DisplayOnSideMenu = _displayOnSideMenu,
			               		SortOrder = _sortOrder,
			               		Url = String.Format(_urlPattern, newsItemId)
			               	};
			Repository.News.Add(newsItem);

			Repository.SaveChanges();
			SiteMapCache.Clear();

			return newsItem.Url;
		}
	}
}