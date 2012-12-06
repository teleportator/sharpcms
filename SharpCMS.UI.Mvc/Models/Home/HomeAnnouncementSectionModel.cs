using System;

namespace SharpCMS.UI.Mvc.Models.Home
{
    public class HomeAnnouncementSectionModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public int ItemsAmount { get; set; }
    }
}