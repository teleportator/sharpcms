using System;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Vacancies
{
	public class DeleteVacancyOperation : Operation
	{
		private readonly Guid _id;

		public DeleteVacancyOperation(Guid id)
		{
			_id = id;
		}

		protected override void Perform()
		{
			Vacancy vacancy = Repository.Vacancies.GetById(_id);
			Repository.Vacancies.Delete(vacancy);

			Repository.SaveChanges();
			SiteMapCache.Clear();
		}
	}
}