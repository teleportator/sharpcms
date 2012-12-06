using System;
using SharpCMS.BusinessLogic.Views;
using SharpCMS.BusinessLogic.Views.Mappers;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Ideas
{
	public class FindIdeaOperation : Operation<IdeaView>
	{
		private readonly Guid _id;

		public FindIdeaOperation(Guid id)
		{
			_id = id;
		}

		protected override IdeaView PerformWithResult()
		{
			Idea idea = Repository.Ideas.GetById(_id);

			return idea != null ? idea.ConvertToIdeaView() : null;
		}
	}
}