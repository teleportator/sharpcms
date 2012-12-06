using System;
using SharpCMS.BusinessLogic.Cache;
using SharpCMS.Repository.EF;

namespace SharpCMS.BusinessLogic
{
	public abstract class BusinessClass : IDisposable
	{
		private readonly bool _isSharedContext;
		protected BusinessClass() : this(false)
		{
			Repository = RepositoryFactory.CreateInstance();
			SiteMapCache = new HttpSiteMapCache();
		}

		protected BusinessClass(IRepository repository) : this(true)
		{
			Repository = repository;
		}

		private BusinessClass(bool isSharedContext)
		{
			_isSharedContext = isSharedContext;
		}

		protected IRepository Repository { get; private set; }
		protected ISiteMapCache SiteMapCache { get; private set; }

		#region IDisposable Members

		public void Dispose()
		{
			if (_isSharedContext) return;

			Repository.Dispose();
		}

		#endregion
	}
}