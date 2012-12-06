namespace SharpCMS.Repository.EF
{
	public class RepositoryFactory
	{
		public static IRepository CreateInstance()
		{
			return new Repository(new CmsContext());
		}
	}
}