using System;

namespace SharpCMS.BusinessLogic.News
{
	public class UpdateNewsItemOperation : Operation<string>
	{
		private readonly bool _displayOnMainMenu;
		private readonly bool _displayOnSideMenu;
		private readonly string _editor;
		private readonly Guid _id;
		private readonly bool _isActive;
		private readonly DateTime _publishedDate;
		private readonly int _sortOrder;
		private readonly string _summary;
		private readonly string _text;
		private readonly string _title;

		public UpdateNewsItemOperation(Guid id, string summary, bool isActive,
		                               string text, string title, string editor, DateTime publishedDate,
		                               bool displayOnMainMenu, bool displayOnSideMenu, int sortOrder)
		{
			_id = id;
			_summary = summary;
			_isActive = isActive;
			_text = text;
			_title = title;
			_editor = editor;
			_publishedDate = publishedDate;
			_displayOnMainMenu = displayOnMainMenu;
			_displayOnSideMenu = displayOnSideMenu;
			_sortOrder = sortOrder;
		}

		protected override string PerformWithResult()
		{
			Domain.News newsItem = Repository.News.GetById(_id);

			newsItem.Abstract = _summary;
			newsItem.IsActive = _isActive;
			newsItem.Text = _text;
			newsItem.Title = _title;
			newsItem.Updated = DateTime.Now;
			newsItem.UpdatedBy = _editor;
			newsItem.PublishedDate = _publishedDate;
			newsItem.DisplayOnMainMenu = _displayOnMainMenu;
			newsItem.DisplayOnSideMenu = _displayOnSideMenu;
			newsItem.SortOrder = _sortOrder;

			Repository.SaveChanges();
			SiteMapCache.Clear();

			return newsItem.Url;
		}
	}
}