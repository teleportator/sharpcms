using System.Data.Entity;
using SharpCMS.Domain;

namespace SharpCMS.Repository.EF
{
	public class CmsContext : DbContext
	{
		public CmsContext()
			: base("name=CmsContext")
		{
		}

		public CmsContext(string connectionString)
			: base(connectionString)
		{
		}

		public DbSet<Content> Contents { get; set; }
		public DbSet<Page> Pages { get; set; }
	}
}