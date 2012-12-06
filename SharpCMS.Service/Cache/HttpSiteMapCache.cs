using System;
using System.Web;
using SharpCMS.Repository.EF;

namespace SharpCMS.BusinessLogic.Cache
{
	public class HttpSiteMapCache : ISiteMapCache
	{
		private const string Key = "SharpCMS_SiteNodeHierarchy";
		private readonly HttpContext _httpContext;
		private readonly IRepository _repository;

		public HttpSiteMapCache()
		{
			_repository = RepositoryFactory.CreateInstance();
			_httpContext = HttpContext.Current;
		}

		#region ISiteMapCache Members

		public void Clear()
		{
			_httpContext.Cache.Remove(Key);
		}

		public Views.SiteMap GetSiteMap()
		{
			Views.SiteMap siteMap;
			if (_httpContext.Cache[Key] != null)
			{
				siteMap = (Views.SiteMap)_httpContext.Cache[Key];
			}
			else
			{
				siteMap = new Views.SiteMap(_repository.Pages.GetAll());
				_httpContext.Cache.Insert(Key, siteMap, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
			}
			return siteMap;
		}

		#endregion
	}
}