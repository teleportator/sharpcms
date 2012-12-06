using System;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Companies
{
	public class AddCompanyOperation : Operation<string>
	{
		private readonly string _address;
		private readonly string _createdBy;
		private readonly bool _displayOnMainMenu;
		private readonly bool _displayOnSideMenu;
		private readonly string _email;
		private readonly string _hyperlink;
		private readonly bool _isActive;
		private readonly string _logoUrl;
		private readonly Guid _parentId;
		private readonly string _phoneNumber;
		private readonly int _sortOrder;
		private readonly string _summary;
		private readonly string _text;
		private readonly string _title;
		private readonly string _urlPattern;

		public AddCompanyOperation(string summary, string createdBy, bool isActive,
		                           Guid parentId, string text, string title, string logoUrl,
		                           string phoneNumber, string address, string email, string hyperlink, bool displayOnMainMenu,
		                           bool displayOnSideMenu, int sortOrder, string urlPattern)
		{
			_summary = summary;
			_createdBy = createdBy;
			_isActive = isActive;
			_parentId = parentId;
			_text = text;
			_title = title;
			_logoUrl = logoUrl;
			_phoneNumber = phoneNumber;
			_address = address;
			_email = email;
			_hyperlink = hyperlink;
			_displayOnMainMenu = displayOnMainMenu;
			_displayOnSideMenu = displayOnSideMenu;
			_sortOrder = sortOrder;
			_urlPattern = urlPattern;
		}

		protected override string PerformWithResult()
		{
			Guid companyId = Guid.NewGuid();
			var company = new Company
			              	{
			              		Abstract = _summary,
			              		Created = DateTime.Now,
			              		CreatedBy = _createdBy,
			              		Id = companyId,
			              		IsActive = _isActive,
			              		ParentId = _parentId,
			              		Text = _text,
			              		Title = _title,
			              		Updated = DateTime.Now,
			              		UpdatedBy = _createdBy,
			              		Logo = _logoUrl,
			              		PhoneNumber = _phoneNumber,
			              		Address = _address,
			              		Email = _email,
								Hyperlink = _hyperlink,
								DisplayOnMainMenu = _displayOnMainMenu,
								DisplayOnSideMenu = _displayOnSideMenu,
								SortOrder = _sortOrder,
								Url = String.Format(_urlPattern, companyId)
			              	};
			Repository.Companies.Add(company);

			Repository.SaveChanges();
			SiteMapCache.Clear();

			return company.Url;
		}
	}
}