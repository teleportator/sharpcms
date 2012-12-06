namespace SharpCMS.Repository.EF.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddPageRelationships : DbMigration
    {
        public override void Up()
        {
            AlterColumn("SharpCMS_Pages", "ParentId", c => c.Guid());

			// Change root ParentId
			Sql("UPDATE dbo.SharpCMS_Pages SET ParentId = NULL WHERE ParentId = '00000000-0000-0000-0000-000000000000'");

			// Delete entities with not existed parents
			Sql("DELETE FROM ch FROM [SharpCMS_Pages] as ch Left OUTER JOIN [SharpCMS_Pages] as pr ON ch.ParentId = pr.Id WHERE ch.ParentId IS NOT NULL AND pr.Id IS NULL");

            AddForeignKey("SharpCMS_Pages", "ParentId", "SharpCMS_Pages", "Id");
            CreateIndex("SharpCMS_Pages", "ParentId");
        }
        
        public override void Down()
        {
            DropIndex("SharpCMS_Pages", new[] { "ParentId" });
            DropForeignKey("SharpCMS_Pages", "ParentId", "SharpCMS_Pages");
            AlterColumn("SharpCMS_Pages", "ParentId", c => c.Guid(nullable: false));
        }
    }
}
