using System;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Articles
{
	public class UpdateArticleOperation : Operation<string>
	{
		private readonly bool _displayOnMainMenu;
		private readonly bool _displayOnSideMenu;
		private readonly string _editor;
		private readonly Guid _id;
		private readonly bool _isActive;
		private readonly int _sortOrder;
		private readonly string _summary;
		private readonly string _text;
		private readonly string _title;

		public UpdateArticleOperation(Guid id, string summary, bool isActive, string text, string title, string editor,
		                              bool displayOnMainMenu, bool displayOnSideMenu, int sortOrder)
		{
			_id = id;
			_summary = summary;
			_isActive = isActive;
			_text = text;
			_title = title;
			_editor = editor;
			_displayOnMainMenu = displayOnMainMenu;
			_displayOnSideMenu = displayOnSideMenu;
			_sortOrder = sortOrder;
		}

		protected override string PerformWithResult()
		{
			Article article = Repository.Articles.GetById(_id);

			article.Abstract = _summary;
			article.IsActive = _isActive;
			article.Text = _text;
			article.Title = _title;
			article.Updated = DateTime.Now;
			article.UpdatedBy = _editor;
			article.DisplayOnMainMenu = _displayOnMainMenu;
			article.DisplayOnSideMenu = _displayOnSideMenu;
			article.SortOrder = _sortOrder;

			Repository.SaveChanges();
			SiteMapCache.Clear();

			return article.Url;
		}
	}
}