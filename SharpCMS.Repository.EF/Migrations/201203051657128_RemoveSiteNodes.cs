namespace SharpCMS.Repository.EF.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RemoveSiteNodes : DbMigration
    {
        public override void Up()
        {
            DropTable("SharpCMS_SiteNodes");
        }
        
        public override void Down()
        {
            CreateTable(
                "SharpCMS_SiteNodes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParentId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Title = c.String(nullable: false, maxLength: 256),
                        Url = c.String(nullable: false),
                        ContentItemId = c.Guid(nullable: false),
                        DisplayOnMainMenu = c.Boolean(nullable: false),
                        DisplayOnSideMenu = c.Boolean(nullable: false),
                        SortOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
