using System;
using System.Collections.Generic;
using SharpCMS.BusinessLogic.Views;

namespace SharpCMS.UI.Mvc.Models.Vacancies
{
	public class VacancyListModel
	{
		public Guid Id { get; set; }
        public IEnumerable<VacancyView> Vacancies { get; set; } 
    }
}