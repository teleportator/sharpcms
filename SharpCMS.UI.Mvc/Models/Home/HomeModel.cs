using System;
using System.Collections.Generic;
using SharpCMS.BusinessLogic.Views;

namespace SharpCMS.UI.Mvc.Models.Home
{
    public class HomeModel
    {
        public Guid Id { get; set; }
        public ArticleView HomePageArticle { get; set; }
        public IEnumerable<NewsView> LatestNews { get; set; }
        public IEnumerable<HomeAnnouncementSectionModel> AnnouncementSections { get; set; }
        public IEnumerable<CompanyView> Companies { get; set; }
    }
}