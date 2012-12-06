using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Mvc;
using SharpCMS.BusinessLogic;
using SharpCMS.BusinessLogic.Announcements;
using SharpCMS.BusinessLogic.Articles;
using SharpCMS.BusinessLogic.Companies;
using SharpCMS.BusinessLogic.News;
using SharpCMS.BusinessLogic.Views;
using SharpCMS.UI.Mvc.Infrastructure;
using SharpCMS.UI.Mvc.Models.Home;

namespace SharpCMS.UI.Mvc.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult Index()
        {
            var model = new HomeModel
                            {
                                Id = GetHomePageArticleId(),
                                LatestNews = GetLatestNews(),
                                AnnouncementSections = GetAnnouncementGroups(),
                                Companies = GetCompanies()
                            };

            model.HomePageArticle = GetHomePageArticle(model.Id);

            return View(model);
        }

        public ActionResult Video()
        {
            return View();
        }

		private IEnumerable<HomeAnnouncementSectionModel> GetAnnouncementGroups()
		{
			IEnumerable<ArticleView> announcementSections =
				BusinessShell.RunWithResult(
					() => new FindArticleCollectionOperation(AllowFullAccess, new Guid("1d8ffab0-0485-47fe-8f53-cbac3c9af423")));

			return announcementSections
				.Select(s => new HomeAnnouncementSectionModel
				             	{
				             		Abstract = s.Abstract,
				             		Id = s.Id,
				             		ItemsAmount = GetCommingAnnouncementsAmount(s.Id, AllowFullAccess),
				             		Title = s.Title
				             	});
		}

    	private int GetCommingAnnouncementsAmount(Guid id, bool allowFullAccess)
    	{
    		return BusinessShell.RunWithResult(() => new GetCommingAnnouncementsAmountOperation(allowFullAccess, id));
    	}

    	private IEnumerable<CompanyView> GetCompanies()
    	{
    		return BusinessShell.RunWithResult(
    			() => new FindCompanyCollectionOperation(AllowFullAccess, new Guid("cf3e7ba9-df4f-4ab3-9fe4-1a1a9ed974ed")))
    			.TakeRandom(4);
    	}

    	private ArticleView GetHomePageArticle(Guid id)
        {
			return BusinessShell.RunWithResult(() => new FindArticleOpertion(id));
        }

        private Guid GetHomePageArticleId()
        {
			return BusinessShell.RunWithResult(() => new GetRootArticleOperation()).Id;
        }

        private IEnumerable<NewsView> GetLatestNews()
        {
        	return
        		BusinessShell.RunWithResult(
        			() => new FindNewsCollectionOperation(AllowFullAccess, new Guid("13a2cfb5-dc64-49a5-8ab9-2f494bb0f85b")))
					.Take(4);
        }
    }
}