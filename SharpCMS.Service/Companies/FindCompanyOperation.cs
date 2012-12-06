using System;
using SharpCMS.BusinessLogic.Views;
using SharpCMS.BusinessLogic.Views.Mappers;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Companies
{
	public class FindCompanyOperation : Operation<CompanyView>
	{
		private readonly Guid _id;

		public FindCompanyOperation(Guid id)
		{
			_id = id;
		}

		protected override CompanyView PerformWithResult()
		{
			Company company = Repository.Companies.GetById(_id);

			return company != null ? company.ConvertToCompanyView() : null;
		}
	}
}