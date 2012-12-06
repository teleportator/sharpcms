using System;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Ideas
{
	public class UpdateIdeaOperation : Operation<string>
	{
		private readonly string _category;
		private readonly bool _displayOnMainMenu;
		private readonly bool _displayOnSideMenu;
		private readonly string _editor;
		private readonly Guid _id;
		private readonly bool _isActive;
		private readonly int _sortOrder;
		private readonly string _summary;
		private readonly string _text;
		private readonly string _title;

		public UpdateIdeaOperation(Guid id, string summary, bool isActive, string text,
		                           string title, string editor, string category, bool displayOnMainMenu,
		                           bool displayOnSideMenu, int sortOrder)
		{
			_id = id;
			_summary = summary;
			_isActive = isActive;
			_text = text;
			_title = title;
			_editor = editor;
			_category = category;
			_displayOnMainMenu = displayOnMainMenu;
			_displayOnSideMenu = displayOnSideMenu;
			_sortOrder = sortOrder;
		}

		protected override string PerformWithResult()
		{
			Idea idea = Repository.Ideas.GetById(_id);
			idea.Abstract = _summary;
			idea.IsActive = _isActive;
			idea.Text = _text;
			idea.Title = _title;
			idea.Updated = DateTime.Now;
			idea.UpdatedBy = _editor;
			idea.Category = _category;
			idea.DisplayOnMainMenu = _displayOnMainMenu;
			idea.DisplayOnSideMenu = _displayOnSideMenu;
			idea.SortOrder = _sortOrder;

			Repository.SaveChanges();
			SiteMapCache.Clear();

			return idea.Url;
		}
	}
}