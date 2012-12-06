namespace SharpCMS.Repository.EF.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class SplitCommentsTable : DbMigration
    {
        public override void Up()
        {
			//Copy to temporary table SharpCMS_Comments_tmp
			Sql("SELECT co.[Id] ,co.[ParentId] ,co.[Created] ,co.[CreatedBy] ,co.[IsActive] ,co.[Text] ,co.[Updated] ,co.[UpdatedBy] INTO SharpCMS_Comments_tmp  FROM [SharpCMS_Comments] as co INNER JOIN SharpCMS_Pages as pa ON co.ParentId = pa.Id");

            DropTable("SharpCMS_Comments");

            CreateTable(
                "SharpCMS_Contents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 256),
                        IsActive = c.Boolean(nullable: false),
                        ParentId = c.Guid(nullable: false),
                        Updated = c.DateTime(nullable: false),
                        UpdatedBy = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("SharpCMS_Pages", t => t.ParentId, cascadeDelete: true)
                .Index(t => t.ParentId);
            
            CreateTable(
                "SharpCMS_Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Text = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
				.ForeignKey("SharpCMS_Contents", t => t.Id, cascadeDelete: true)
				.Index(t => t.Id);

			//Move data from temporary table to new tables
			Sql("INSERT SharpCMS_Contents (Id, Created, CreatedBy, IsActive, ParentId, Updated, UpdatedBy) SELECT Id, Created, CreatedBy, IsActive, ParentId, Updated, UpdatedBy FROM SharpCMS_Comments_tmp");
			Sql("INSERT SharpCMS_Comments (Id, Text) SELECT Id, Text FROM SharpCMS_Comments_tmp");

			DropTable("SharpCMS_Comments_tmp");
        }
        
        public override void Down()
        {
            CreateTable(
                "SharpCMS_Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Text = c.String(nullable: false),
                        ParentId = c.Guid(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 256),
                        Updated = c.DateTime(nullable: false),
                        UpdatedBy = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            DropIndex("SharpCMS_Comments", new[] { "Id" });
            DropIndex("SharpCMS_Contents", new[] { "ParentId" });
            DropForeignKey("SharpCMS_Comments", "Id", "SharpCMS_Contents");
            DropForeignKey("SharpCMS_Contents", "ParentId", "SharpCMS_Pages");
            DropTable("SharpCMS_Comments");
            DropTable("SharpCMS_Contents");
        }
    }
}
