using System;
using System.Collections.Generic;
using SharpCMS.BusinessLogic.Views;

namespace SharpCMS.UI.Mvc.Models.Companies
{
	public class CompanyListModel
	{
		public Guid Id { get; set; }
		public IEnumerable<CompanyView> Companies { get; set; }
	}
}