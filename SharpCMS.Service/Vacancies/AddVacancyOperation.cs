using System;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Vacancies
{
	public class AddVacancyOperation : Operation<string>
	{
		private readonly string _conditions;
		private readonly string _contact;
		private readonly string _createdBy;
		private readonly string _demands;
		private readonly bool _displayOnMainMenu;
		private readonly bool _displayOnSideMenu;
		private readonly string _employer;
		private readonly bool _isActive;
		private readonly Guid _parentId;
		private readonly string _responsibilities;
		private readonly int _sortOrder;
		private readonly string _summary;
		private readonly string _text;
		private readonly string _title;
		private readonly string _urlPattern;

		public AddVacancyOperation(string summary, string createdBy, bool isActive,
		                           Guid parentId, string text, string title, string employer, string contact,
		                           string responsibilities, string conditions, string demands, bool displayOnMainMenu,
		                           bool displayOnSideMenu, int sortOrder, string urlPattern)
		{
			_summary = summary;
			_createdBy = createdBy;
			_isActive = isActive;
			_parentId = parentId;
			_text = text;
			_title = title;
			_employer = employer;
			_contact = contact;
			_responsibilities = responsibilities;
			_conditions = conditions;
			_demands = demands;
			_displayOnMainMenu = displayOnMainMenu;
			_displayOnSideMenu = displayOnSideMenu;
			_sortOrder = sortOrder;
			_urlPattern = urlPattern;
		}

		protected override string PerformWithResult()
		{
			Guid vacancyId = Guid.NewGuid();
			var vacancy = new Vacancy
			              	{
			              		Abstract = _summary,
			              		Created = DateTime.Now,
			              		CreatedBy = _createdBy,
			              		Id = vacancyId,
			              		IsActive = _isActive,
			              		ParentId = _parentId,
			              		Text = _text,
			              		Title = _title,
			              		Updated = DateTime.Now,
			              		UpdatedBy = _createdBy,
			              		Employer = _employer,
			              		Contact = _contact,
			              		Responsibilities = _responsibilities,
			              		Conditions = _conditions,
			              		Demands = _demands,
			              		DisplayOnMainMenu = _displayOnMainMenu,
			              		DisplayOnSideMenu = _displayOnSideMenu,
			              		SortOrder = _sortOrder,
			              		Url = String.Format(_urlPattern, vacancyId)
			              	};
			Repository.Vacancies.Add(vacancy);

			Repository.SaveChanges();
			SiteMapCache.Clear();

			return vacancy.Url;
		}
	}
}