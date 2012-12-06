using System;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Companies
{
	public class DeleteCompanyOperation : Operation
	{
		private readonly Guid _id;

		public DeleteCompanyOperation(Guid id)
		{
			_id = id;
		}

		protected override void Perform()
		{
			Company company = Repository.Companies.GetById(_id);
			Repository.Companies.Delete(company);

			Repository.SaveChanges();
			SiteMapCache.Clear();
		}
	}
}