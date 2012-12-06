using System;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Companies
{
	public class UpdateCompanyOperation : Operation<string>
	{
		private readonly string _address;
		private readonly bool _displayOnMainMenu;
		private readonly bool _displayOnSideMenu;
		private readonly string _editor;
		private readonly string _email;
		private readonly string _hyperlink;
		private readonly Guid _id;
		private readonly bool _isActive;
		private readonly string _logoUrl;
		private readonly string _phoneNumber;
		private readonly int _sortOrder;
		private readonly string _summary;
		private readonly string _text;
		private readonly string _title;

		public UpdateCompanyOperation(Guid id, string summary, bool isActive, string text,
		                              string title, string editor, string logoUrl, string phoneNumber, string address,
		                              string email, string hyperlink, bool displayOnMainMenu, bool displayOnSideMenu,
		                              int sortOrder)
		{
			_id = id;
			_summary = summary;
			_isActive = isActive;
			_text = text;
			_title = title;
			_editor = editor;
			_logoUrl = logoUrl;
			_phoneNumber = phoneNumber;
			_address = address;
			_email = email;
			_hyperlink = hyperlink;
			_displayOnMainMenu = displayOnMainMenu;
			_displayOnSideMenu = displayOnSideMenu;
			_sortOrder = sortOrder;
		}

		protected override string PerformWithResult()
		{
			Company company = Repository.Companies.GetById(_id);

			company.Abstract = _summary;
			company.IsActive = _isActive;
			company.Text = _text;
			company.Title = _title;
			company.Updated = DateTime.Now;
			company.UpdatedBy = _editor;
			company.Logo = _logoUrl;
			company.PhoneNumber = _phoneNumber;
			company.Address = _address;
			company.Email = _email;
			company.Hyperlink = _hyperlink;
			company.DisplayOnMainMenu = _displayOnMainMenu;
			company.DisplayOnSideMenu = _displayOnSideMenu;
			company.SortOrder = _sortOrder;

			Repository.SaveChanges();
			SiteMapCache.Clear();

			return company.Url;
		}
	}
}