using System;
using System.Collections.Generic;
using System.Linq;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Views.Mappers
{
	public static class CompanyExtensions
	{
		public static CompanyView ConvertToCompanyView(this Company company)
		{
			if (company == null)
				throw new ArgumentNullException();

			return new CompanyView
			       	{
			       		Id = company.Id,
			       		Title = company.Title,
			       		ParentId = company.ParentId,
			       		Abstract = company.Abstract,
			       		Created = company.Created,
			       		CreatedBy = company.CreatedBy,
			       		IsActive = company.IsActive,
			       		Text = company.Text,
			       		Updated = company.Updated,
			       		UpdatedBy = company.UpdatedBy,
			       		Logo = company.Logo,
			       		PhoneNumber = company.PhoneNumber,
			       		Address = company.Address,
			       		Email = company.Email,
						Hyperlink = company.Hyperlink,
						DisplayOnMainMenu = company.DisplayOnMainMenu,
						DisplayOnSideMenu = company.DisplayOnSideMenu,
						SortOrder = company.SortOrder,
						Url = company.Url
			       	};
		}

		public static IEnumerable<CompanyView> ConvertToCompanyViewCollection(this IEnumerable<Company> companyList)
		{
			return companyList.Select(c => c.ConvertToCompanyView()).ToList();
		}
	}
}