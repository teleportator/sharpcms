using System;
using System.Collections.Generic;
using System.Linq;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Views.Mappers
{
	public static class VacancyExtensions
	{
		public static VacancyView ConvertToVacancyView(this Vacancy vacancy)
		{
			if (vacancy == null)
				throw new ArgumentNullException();

			return new VacancyView
			       	{
			       		Id = vacancy.Id,
			       		Title = vacancy.Title,
			       		ParentId = vacancy.ParentId,
			       		Abstract = vacancy.Abstract,
			       		Created = vacancy.Created,
			       		CreatedBy = vacancy.CreatedBy,
			       		IsActive = vacancy.IsActive,
			       		Text = vacancy.Text,
			       		Updated = vacancy.Updated,
			       		UpdatedBy = vacancy.UpdatedBy,
			       		Employer = vacancy.Employer,
			       		Contact = vacancy.Contact,
			       		Responsibilities = vacancy.Responsibilities,
			       		Demands = vacancy.Demands,
						Conditions = vacancy.Conditions,
						DisplayOnMainMenu = vacancy.DisplayOnMainMenu,
						DisplayOnSideMenu = vacancy.DisplayOnSideMenu,
						SortOrder = vacancy.SortOrder,
						Url = vacancy.Url
			       	};
		}

		public static IEnumerable<VacancyView> ConvertToVacancyViewCollection(this IEnumerable<Vacancy> vacancies)
		{
			return vacancies.Select(v => v.ConvertToVacancyView()).ToList();
		}
	}
}