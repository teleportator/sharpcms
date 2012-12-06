using System;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Vacancies
{
	public class UpdateVacancyOperation : Operation<string>
	{
		private readonly string _conditions;
		private readonly string _contact;
		private readonly string _demands;
		private readonly bool _displayOnMainMenu;
		private readonly bool _displayOnSideMenu;
		private readonly string _editor;
		private readonly string _employer;
		private readonly Guid _id;
		private readonly bool _isActive;
		private readonly string _responsibilities;
		private readonly int _sortOrder;
		private readonly string _summary;
		private readonly string _text;
		private readonly string _title;

		public UpdateVacancyOperation(Guid id, string summary, bool isActive, string text, string title, string editor,
		                              string employer, string contact, string responsibilities, string demands,
		                              string conditions, bool displayOnMainMenu, bool displayOnSideMenu, int sortOrder)
		{
			_id = id;
			_summary = summary;
			_isActive = isActive;
			_text = text;
			_title = title;
			_editor = editor;
			_employer = employer;
			_contact = contact;
			_responsibilities = responsibilities;
			_demands = demands;
			_conditions = conditions;
			_displayOnMainMenu = displayOnMainMenu;
			_displayOnSideMenu = displayOnSideMenu;
			_sortOrder = sortOrder;
		}

		protected override string PerformWithResult()
		{
			Vacancy vacancy = Repository.Vacancies.GetById(_id);

			vacancy.Abstract = _summary;
			vacancy.IsActive = _isActive;
			vacancy.Text = _text;
			vacancy.Title = _title;
			vacancy.Updated = DateTime.Now;
			vacancy.UpdatedBy = _editor;
			vacancy.Employer = _employer;
			vacancy.Contact = _contact;
			vacancy.Responsibilities = _responsibilities;
			vacancy.Demands = _demands;
			vacancy.Conditions = _conditions;
			vacancy.DisplayOnMainMenu = _displayOnMainMenu;
			vacancy.DisplayOnSideMenu = _displayOnSideMenu;
			vacancy.SortOrder = _sortOrder;

			Repository.SaveChanges();
			SiteMapCache.Clear();

			return vacancy.Url;
		}
	}
}