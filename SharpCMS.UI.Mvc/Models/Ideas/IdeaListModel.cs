using System;
using System.Collections.Generic;
using SharpCMS.BusinessLogic.Views;

namespace SharpCMS.UI.Mvc.Models.Ideas
{
    public class IdeaListModel
	{
		public Guid Id { get; set; }
        public IEnumerable<IdeaView> Ideas { get; set; }
    }
}