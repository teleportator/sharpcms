using System;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Ideas
{
	public class AddIdeaOperation : Operation<string>
	{
		private readonly string _category;
		private readonly string _createdBy;
		private readonly bool _displayOnMainMenu;
		private readonly bool _displayOnSideMenu;
		private readonly bool _isActive;
		private readonly Guid _parentId;
		private readonly int _rating;
		private readonly int _sortOrder;
		private readonly string _summary;
		private readonly string _text;
		private readonly string _title;
		private readonly string _urlPattern;

		public AddIdeaOperation(string summary, string createdBy, bool isActive,
		                        Guid parentId, string text, string title, string category, int rating, bool displayOnMainMenu,
		                        bool displayOnSideMenu, int sortOrder, string urlPattern)
		{
			_summary = summary;
			_createdBy = createdBy;
			_isActive = isActive;
			_parentId = parentId;
			_text = text;
			_title = title;
			_category = category;
			_rating = rating;
			_displayOnMainMenu = displayOnMainMenu;
			_displayOnSideMenu = displayOnSideMenu;
			_sortOrder = sortOrder;
			_urlPattern = urlPattern;
		}

		protected override string PerformWithResult()
		{
			Guid ideaId = Guid.NewGuid();
			var idea = new Idea
			           	{
			           		Abstract = _summary,
			           		Created = DateTime.Now,
			           		CreatedBy = _createdBy,
			           		Id = ideaId,
			           		IsActive = _isActive,
			           		ParentId = _parentId,
			           		Text = _text,
			           		Title = _title,
			           		Updated = DateTime.Now,
			           		UpdatedBy = _createdBy,
			           		Category = _category,
							Rating = _rating,
							DisplayOnMainMenu = _displayOnMainMenu,
							DisplayOnSideMenu = _displayOnSideMenu,
							SortOrder = _sortOrder,
							Url = String.Format(_urlPattern, ideaId)
			           	};
			Repository.Ideas.Add(idea);

			Repository.SaveChanges();
			SiteMapCache.Clear();

			return idea.Url;
		}
	}
}