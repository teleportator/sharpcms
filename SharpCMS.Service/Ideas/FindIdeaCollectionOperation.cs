using System;
using System.Collections.Generic;
using System.Linq;
using SharpCMS.BusinessLogic.Views;
using SharpCMS.BusinessLogic.Views.Mappers;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Ideas
{
	public class FindIdeaCollectionOperation : Operation<IEnumerable<IdeaView>>
	{
		private readonly bool _showInactive;
		private readonly Guid _parentId;

		public FindIdeaCollectionOperation(bool showInactive, Guid parentId)
		{
			_showInactive = showInactive;
			_parentId = parentId;
		}

		protected override IEnumerable<IdeaView> PerformWithResult()
		{
			List<Idea> ideas = _showInactive
			                   	? Repository.Ideas.Find(i => i.ParentId == _parentId).ToList()
			                   	: Repository.Ideas.Find(i => (i.ParentId == _parentId) && i.IsActive).ToList();

			return ideas.OrderByDescending(n => n.Created).ConvertToIdeaViewCollection();
		}
	}
}