namespace SharpCMS.Repository.EF.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class MergePageAndSiteNodeEntities : DbMigration
    {
        public override void Up()
        {
            // Copy to temporary tables
            Sql("SELECT * INTO SharpCMS_Announcements_tmp FROM SharpCMS_Announcements SELECT * INTO SharpCMS_Articles_tmp FROM SharpCMS_Articles SELECT * INTO SharpCMS_Companies_tmp FROM SharpCMS_Companies SELECT * INTO SharpCMS_Ideas_tmp FROM SharpCMS_Ideas SELECT * INTO SharpCMS_News_tmp FROM SharpCMS_News SELECT * INTO SharpCMS_Vacancies_tmp FROM SharpCMS_Vacancies");

            DropTable("SharpCMS_Articles");
            DropTable("SharpCMS_News");
            DropTable("SharpCMS_Companies");
            DropTable("SharpCMS_Ideas");
            DropTable("SharpCMS_Vacancies");
            DropTable("SharpCMS_Announcements");

            CreateTable(
                "SharpCMS_Pages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Abstract = c.String(nullable: false, maxLength: 256),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 256),
                        DisplayOnMainMenu = c.Boolean(nullable: false),
                        DisplayOnSideMenu = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ParentId = c.Guid(nullable: false),
                        SortOrder = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 256),
                        Text = c.String(nullable: false),
                        Url = c.String(nullable: false, maxLength: 1024),
                        Updated = c.DateTime(nullable: false),
                        UpdatedBy = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SharpCMS_News",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PublishedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SharpCMS_Pages", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "SharpCMS_Vacancies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Employer = c.String(nullable: false, maxLength: 256),
                        Contact = c.String(nullable: false, maxLength: 256),
                        Responsibilities = c.String(nullable: false, maxLength: 1024),
                        Demands = c.String(nullable: false, maxLength: 1024),
                        Conditions = c.String(nullable: false, maxLength: 1024),
                    })
                .PrimaryKey(t => t.Id)
				.ForeignKey("SharpCMS_Pages", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "SharpCMS_Articles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
				.ForeignKey("SharpCMS_Pages", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "SharpCMS_Companies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Logo = c.String(nullable: false, maxLength: 256),
                        PhoneNumber = c.String(nullable: false, maxLength: 256),
                        Address = c.String(nullable: false, maxLength: 256),
                        Email = c.String(nullable: false, maxLength: 256),
                        Hyperlink = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
				.ForeignKey("SharpCMS_Pages", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "SharpCMS_Announcements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StartingDate = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        Venue = c.String(nullable: false, maxLength: 256),
                        StartingTime = c.String(nullable: false, maxLength: 256),
                        Organizer = c.String(nullable: false, maxLength: 256),
                        Contact = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
				.ForeignKey("SharpCMS_Pages", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "SharpCMS_Ideas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Category = c.String(nullable: false, maxLength: 256),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
				.ForeignKey("SharpCMS_Pages", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);

            // Move data from temporary tables
			Sql("Insert SharpCMS_Pages (Id ,Abstract ,Created ,CreatedBy ,IsActive ,ParentId ,Title ,Text ,Updated ,UpdatedBy ,DisplayOnMainMenu ,DisplayOnSidemenu ,SortOrder ,Url) SELECT an.Id ,an.Abstract ,an.Created ,an.CreatedBy ,an.IsActive ,an.ParentId ,an.Title ,an.Text ,an.Updated ,an.UpdatedBy ,sn.DisplayOnMainMenu ,sn.DisplayOnSidemenu ,sn.SortOrder ,sn.Url FROM [SharpCMS_SiteNodes] as sn RIGHT OUTER JOIN dbo.SharpCMS_Announcements_tmp as an  ON sn.ContentItemId = an.Id  Insert SharpCMS_Announcements (Id ,StartingDate ,ExpiryDate ,Venue ,StartingTime ,Organizer ,Contact) SELECT an.Id ,an.StartingDate ,an.ExpiryDate ,an.Venue ,an.StartingTime ,an.Organizer ,an.Contact FROM [SharpCMS_SiteNodes] as sn RIGHT OUTER JOIN dbo.SharpCMS_Announcements_tmp as an  ON sn.ContentItemId = an.Id");
			Sql("Insert SharpCMS_Pages (Id ,Abstract ,Created ,CreatedBy ,IsActive ,ParentId ,Title ,Text ,Updated ,UpdatedBy ,DisplayOnMainMenu ,DisplayOnSidemenu ,SortOrder ,Url) SELECT ar.Id ,ar.Abstract ,ar.Created ,ar.CreatedBy ,ar.IsActive ,ar.ParentId ,ar.Title ,ar.Text ,ar.Updated ,ar.UpdatedBy ,sn.DisplayOnMainMenu ,sn.DisplayOnSidemenu ,sn.SortOrder ,sn.Url FROM [SharpCMS_SiteNodes] as sn RIGHT OUTER JOIN dbo.SharpCMS_Articles_tmp as ar  ON sn.ContentItemId = ar.Id Insert dbo.SharpCMS_Articles (Id) SELECT ar.Id FROM [SharpCMS_SiteNodes] as sn RIGHT OUTER JOIN dbo.SharpCMS_Articles_tmp as ar  ON sn.ContentItemId = ar.Id");
			Sql("Insert SharpCMS_Pages (Id ,Abstract ,Created ,CreatedBy ,IsActive ,ParentId ,Title ,Text ,Updated ,UpdatedBy ,DisplayOnMainMenu ,DisplayOnSidemenu ,SortOrder ,Url) SELECT co.Id ,co.Abstract ,co.Created ,co.CreatedBy ,co.IsActive ,co.ParentId ,co.Title ,co.Text ,co.Updated ,co.UpdatedBy ,sn.DisplayOnMainMenu ,sn.DisplayOnSidemenu ,sn.SortOrder ,sn.Url FROM [SharpCMS_SiteNodes] as sn RIGHT OUTER JOIN SharpCMS_Companies_tmp as co  ON sn.ContentItemId = co.Id  Insert SharpCMS_Companies (Id ,Logo ,PhoneNumber ,Address ,Email ,Hyperlink) SELECT co.Id ,co.Logo ,co.PhoneNumber ,co.Address ,co.Email ,co.Hyperlink FROM [SharpCMS_SiteNodes] as sn RIGHT OUTER JOIN SharpCMS_Companies_tmp as co ON sn.ContentItemId = co.Id");
			Sql("Insert SharpCMS_Pages (Id ,Abstract ,Created ,CreatedBy ,IsActive ,ParentId ,Title ,Text ,Updated ,UpdatedBy ,DisplayOnMainMenu ,DisplayOnSidemenu ,SortOrder ,Url) SELECT ia.Id ,ia.Abstract ,ia.Created ,ia.CreatedBy ,ia.IsActive ,ia.ParentId ,ia.Title ,ia.Text ,ia.Updated ,ia.UpdatedBy ,sn.DisplayOnMainMenu ,sn.DisplayOnSidemenu ,sn.SortOrder ,sn.Url FROM [SharpCMS_SiteNodes] as sn RIGHT OUTER JOIN SharpCMS_Ideas_tmp as ia  ON sn.ContentItemId = ia.Id  Insert SharpCMS_Ideas (Id ,Category ,Rating) SELECT ia.Id ,ia.Category ,ia.Rating FROM [SharpCMS_SiteNodes] as sn RIGHT OUTER JOIN SharpCMS_Ideas_tmp as ia  ON sn.ContentItemId = ia.Id");
			Sql("Insert SharpCMS_Pages (Id ,Abstract ,Created ,CreatedBy ,IsActive ,ParentId ,Title ,Text ,Updated ,UpdatedBy ,DisplayOnMainMenu ,DisplayOnSidemenu ,SortOrder ,Url) SELECT ns.Id ,ns.Abstract ,ns.Created ,ns.CreatedBy ,ns.IsActive ,ns.ParentId ,ns.Title ,ns.Text ,ns.Updated ,ns.UpdatedBy ,sn.DisplayOnMainMenu ,sn.DisplayOnSidemenu ,sn.SortOrder ,sn.Url FROM [SharpCMS_SiteNodes] as sn RIGHT OUTER JOIN SharpCMS_News_tmp as ns  ON sn.ContentItemId = ns.Id  Insert SharpCMS_News (Id ,PublishedDate) SELECT ns.Id ,ns.PublishedDate FROM [SharpCMS_SiteNodes] as sn RIGHT OUTER JOIN SharpCMS_News_tmp as ns  ON sn.ContentItemId = ns.Id");  
			Sql("Insert SharpCMS_Pages (Id ,Abstract ,Created ,CreatedBy ,IsActive ,ParentId ,Title ,Text ,Updated ,UpdatedBy ,DisplayOnMainMenu ,DisplayOnSidemenu ,SortOrder ,Url) SELECT va.Id ,va.Abstract ,va.Created ,va.CreatedBy ,va.IsActive ,va.ParentId ,va.Title ,va.Text ,va.Updated ,va.UpdatedBy ,sn.DisplayOnMainMenu ,sn.DisplayOnSidemenu ,sn.SortOrder ,sn.Url FROM [SharpCMS_SiteNodes] as sn RIGHT OUTER JOIN SharpCMS_Vacancies_tmp as va  ON sn.ContentItemId = va.Id  Insert SharpCMS_Vacancies (Id ,Employer ,Contact ,Responsibilities ,Demands ,Conditions) SELECT va.Id ,va.Employer ,va.Contact ,va.Responsibilities ,va.Demands ,va.Conditions FROM [SharpCMS_SiteNodes] as sn RIGHT OUTER JOIN SharpCMS_Vacancies_tmp as va  ON sn.ContentItemId = va.Id");

			//Drop temporary tables
            DropTable("SharpCMS_Articles_tmp");
            DropTable("SharpCMS_News_tmp");
            DropTable("SharpCMS_Companies_tmp");
            DropTable("SharpCMS_Ideas_tmp");
            DropTable("SharpCMS_Vacancies_tmp");
            DropTable("SharpCMS_Announcements_tmp");
        }
        
        public override void Down()
        {
            DropIndex("SharpCMS_Ideas", new[] { "Id" });
            DropIndex("SharpCMS_Announcements", new[] { "Id" });
            DropIndex("SharpCMS_Companies", new[] { "Id" });
            DropIndex("SharpCMS_Articles", new[] { "Id" });
            DropIndex("SharpCMS_Vacancies", new[] { "Id" });
            DropIndex("SharpCMS_News", new[] { "Id" });
            DropForeignKey("SharpCMS_Ideas", "Id", "SharpCMS_Pages");
            DropForeignKey("SharpCMS_Announcements", "Id", "SharpCMS_Pages");
            DropForeignKey("SharpCMS_Companies", "Id", "SharpCMS_Pages");
            DropForeignKey("SharpCMS_Articles", "Id", "SharpCMS_Pages");
            DropForeignKey("SharpCMS_Vacancies", "Id", "SharpCMS_Pages");
            DropForeignKey("SharpCMS_News", "Id", "SharpCMS_Pages");
            DropTable("SharpCMS_Ideas");
            DropTable("SharpCMS_Announcements");
            DropTable("SharpCMS_Companies");
            DropTable("SharpCMS_Articles");
            DropTable("SharpCMS_Vacancies");
            DropTable("SharpCMS_News");
            DropTable("SharpCMS_Pages");

            CreateTable(
                "SharpCMS_Announcements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StartingDate = c.DateTime(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                        Venue = c.String(nullable: false, maxLength: 256),
                        StartingTime = c.String(nullable: false, maxLength: 256),
                        Organizer = c.String(nullable: false, maxLength: 256),
                        Contact = c.String(nullable: false, maxLength: 256),
                        Abstract = c.String(nullable: false, maxLength: 256),
                        Title = c.String(nullable: false, maxLength: 256),
                        Text = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 256),
                        Updated = c.DateTime(nullable: false),
                        UpdatedBy = c.String(nullable: false, maxLength: 256),
                        ParentId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SharpCMS_Vacancies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Employer = c.String(nullable: false, maxLength: 256),
                        Contact = c.String(nullable: false, maxLength: 256),
                        Responsibilities = c.String(nullable: false, maxLength: 1024),
                        Demands = c.String(nullable: false, maxLength: 1024),
                        Conditions = c.String(nullable: false, maxLength: 1024),
                        Abstract = c.String(nullable: false, maxLength: 256),
                        Title = c.String(nullable: false, maxLength: 256),
                        Text = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 256),
                        Updated = c.DateTime(nullable: false),
                        UpdatedBy = c.String(nullable: false, maxLength: 256),
                        ParentId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SharpCMS_Ideas",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Category = c.String(nullable: false, maxLength: 256),
                        Rating = c.Int(nullable: false),
                        Abstract = c.String(nullable: false, maxLength: 256),
                        Title = c.String(nullable: false, maxLength: 256),
                        Text = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 256),
                        Updated = c.DateTime(nullable: false),
                        UpdatedBy = c.String(nullable: false, maxLength: 256),
                        ParentId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SharpCMS_Companies",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Logo = c.String(nullable: false, maxLength: 256),
                        PhoneNumber = c.String(nullable: false, maxLength: 256),
                        Address = c.String(nullable: false, maxLength: 256),
                        Email = c.String(nullable: false, maxLength: 256),
                        Hyperlink = c.String(nullable: false, maxLength: 256),
                        Abstract = c.String(nullable: false, maxLength: 256),
                        Title = c.String(nullable: false, maxLength: 256),
                        Text = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 256),
                        Updated = c.DateTime(nullable: false),
                        UpdatedBy = c.String(nullable: false, maxLength: 256),
                        ParentId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SharpCMS_News",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PublishedDate = c.DateTime(nullable: false),
                        Abstract = c.String(nullable: false, maxLength: 256),
                        Title = c.String(nullable: false, maxLength: 256),
                        Text = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 256),
                        Updated = c.DateTime(nullable: false),
                        UpdatedBy = c.String(nullable: false, maxLength: 256),
                        ParentId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "SharpCMS_Articles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Abstract = c.String(nullable: false, maxLength: 256),
                        Title = c.String(nullable: false, maxLength: 256),
                        Text = c.String(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 256),
                        Updated = c.DateTime(nullable: false),
                        UpdatedBy = c.String(nullable: false, maxLength: 256),
                        ParentId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
        }
    }
}
