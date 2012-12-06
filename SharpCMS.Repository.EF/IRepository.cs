using System;
using SharpCMS.Domain;

namespace SharpCMS.Repository.EF
{
    public interface IRepository : IDisposable
    {
        IDao<Announcement, Guid> Announcements { get; }
		IDao<Article, Guid> Articles { get; }
		IDao<Comment, Guid> Comments { get; }
        IDao<Company, Guid> Companies { get; }
        IDao<Idea, Guid> Ideas { get; }
        IDao<News, Guid> News { get; }
        IDao<Vacancy, Guid> Vacancies { get; }
    	IDao<Page, Guid> Pages { get; }

    	void SaveChanges();
    }
}