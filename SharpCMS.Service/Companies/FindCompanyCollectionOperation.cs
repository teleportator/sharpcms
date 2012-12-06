using System;
using System.Collections.Generic;
using System.Linq;
using SharpCMS.BusinessLogic.Views;
using SharpCMS.BusinessLogic.Views.Mappers;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Companies
{
	public class FindCompanyCollectionOperation : Operation<IEnumerable<CompanyView>>
	{
		private readonly bool _showInactive;
		private readonly Guid _parentId;

		public FindCompanyCollectionOperation(bool showInactive, Guid parentId)
		{
			_showInactive = showInactive;
			_parentId = parentId;
		}

		protected override IEnumerable<CompanyView> PerformWithResult()
		{
			List<Company> companies =
				_showInactive
					? Repository.Companies.Find(c => c.ParentId == _parentId).ToList()
					: Repository.Companies.Find(c => (c.ParentId == _parentId) && c.IsActive).ToList();

			return companies.OrderBy(n => n.Title).ConvertToCompanyViewCollection();
		}
	}
}