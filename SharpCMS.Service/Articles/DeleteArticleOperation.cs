using System;
using SharpCMS.Domain;

namespace SharpCMS.BusinessLogic.Articles
{
	public class DeleteArticleOperation : Operation
	{
		private readonly Guid _id;

		public DeleteArticleOperation(Guid id)
		{
			_id = id;
		}

		protected override void Perform()
		{
			Article article = Repository.Articles.GetById(_id);
			Repository.Articles.Delete(article);

			Repository.SaveChanges();
			SiteMapCache.Clear();
		}
	}
}