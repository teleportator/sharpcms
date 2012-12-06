using System;
using System.Collections.Generic;
using System.Linq;
using SharpCMS.BusinessLogic.Views;
using SharpCMS.BusinessLogic.Views.Mappers;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Vacancies
{
	public class FindVacancyCollectionOperation : Operation<IEnumerable<VacancyView>>
	{
		private readonly bool _showInactive;
		private readonly Guid _parentId;

		public FindVacancyCollectionOperation(bool showInactive, Guid parentId)
		{
			_showInactive = showInactive;
			_parentId = parentId;
		}

		protected override IEnumerable<VacancyView> PerformWithResult()
		{
			List<Vacancy> vacancies =
				_showInactive
					? Repository.Vacancies.Find(v => v.ParentId == _parentId).ToList()
					: Repository.Vacancies.Find(v => (v.ParentId == _parentId) && v.IsActive).ToList();

			return vacancies.OrderByDescending(n => n.Created).ConvertToVacancyViewCollection();
		}
	}
}