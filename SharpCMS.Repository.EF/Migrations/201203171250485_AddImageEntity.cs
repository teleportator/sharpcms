namespace SharpCMS.Repository.EF.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddImageEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "SharpCMS_Images",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Source = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SharpCMS_Contents", t => t.Id, cascadeDelete:true)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("SharpCMS_Images", new[] { "Id" });
            DropForeignKey("SharpCMS_Images", "Id", "SharpCMS_Contents");
            DropTable("SharpCMS_Images");
        }
    }
}
