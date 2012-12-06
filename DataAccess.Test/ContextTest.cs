using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpCMS.Repository.EF;

namespace SharpCMS.DataAccess.Tests
{
	[TestClass]
	public class ContextTest
	{
		[TestMethod]
		public void CreateDbTest()
		{
			const string connectionString = "Data Source=localhost;Initial Catalog=Sharp_Cms;uid=billing;pwd=billing";
			Database.SetInitializer(new DropCreateDatabaseAlways<CmsContext>());
			using (var context = new CmsContext(connectionString))
			{
				var query = context.Pages.Select(a => new { a.Id });
				string str = query.ToString();
				var articles = query.ToArray();
			}
		}
	}
}
