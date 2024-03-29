using System;
using System.Collections.Generic;
using SharpCMS.BusinessLogic.Views;

namespace SharpCMS.UI.Mvc.Models.News
{
	public class NewsListModel
    {
		public Guid Id { get; set; }
        public IEnumerable<NewsView> News { get; set; }
    }
}