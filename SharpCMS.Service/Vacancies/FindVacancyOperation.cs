using System;
using SharpCMS.BusinessLogic.Views;
using SharpCMS.BusinessLogic.Views.Mappers;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Vacancies
{
	public class FindVacancyOperation : Operation<VacancyView>
	{
		private readonly Guid _id;

		public FindVacancyOperation(Guid id)
		{
			_id = id;
		}

		protected override VacancyView PerformWithResult()
		{
			Vacancy vacancy = Repository.Vacancies.GetById(_id);

			return vacancy != null ? vacancy.ConvertToVacancyView() : null;
		}
	}
}