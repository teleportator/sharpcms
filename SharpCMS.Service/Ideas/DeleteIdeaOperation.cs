using System;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Ideas
{
	public class DeleteIdeaOperation : Operation
	{
		private readonly Guid _id;

		public DeleteIdeaOperation(Guid id)
		{
			_id = id;
		}

		protected override void Perform()
		{
			Idea idea = Repository.Ideas.GetById(_id);
			Repository.Ideas.Delete(idea);

			Repository.SaveChanges();
			SiteMapCache.Clear();
		}
	}
}