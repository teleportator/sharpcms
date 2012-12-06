using System;
using SharpCMS.Domain;

namespace SharpCMS.Repository.EF
{
	public class Repository : IRepository
	{
		private readonly CmsContext _context;

		public Repository(CmsContext context)
		{
			_context = context;
		}

		public void Dispose()
		{
			_context.Dispose();
		}

		public IDao<Announcement, Guid> Announcements
		{
			get { return new CmsRepositoryBase<Announcement, Guid>(_context); }
		}

		public IDao<Article, Guid> Articles
		{
			get { return new CmsRepositoryBase<Article, Guid>(_context); }
		}

		public IDao<Comment, Guid> Comments
		{
			get { return new CmsRepositoryBase<Comment, Guid>(_context); }
		}

		public IDao<Company, Guid> Companies
		{
			get { return new CmsRepositoryBase<Company, Guid>(_context); }
		}

		public IDao<Idea, Guid> Ideas
		{
			get { return new CmsRepositoryBase<Idea, Guid>(_context); }
		}

		public IDao<News, Guid> News
		{
			get { return new CmsRepositoryBase<News, Guid>(_context); }
		}

		public IDao<Vacancy, Guid> Vacancies
		{
			get { return new CmsRepositoryBase<Vacancy, Guid>(_context); }
		}

		public IDao<Page, Guid> Pages
		{
			get { return new CmsRepositoryBase<Page, Guid>(_context);}
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}
	}
}