using SharpCMS.BusinessLogic.Views;
using SharpCMS.Domain;
using SharpCMS.BusinessLogic.Views.Mappers;

namespace SharpCMS.BusinessLogic.Articles
{
	public class GetRootArticleOperation : Operation<ArticleView>
	{
		protected override ArticleView PerformWithResult()
		{
			Article article = Repository.Articles.FirstOrDefault(a => a.ParentId == null);
			return article != null ? article.ConvertToArticleView() : null;
		}
	}
}