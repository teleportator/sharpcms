// <auto-generated />
namespace SharpCMS.Repository.EF.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    
    public sealed partial class AddImageEntity : IMigrationMetadata
    {
        string IMigrationMetadata.Id
        {
            get { return "201203171250485_AddImageEntity"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return "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"; }
        }
    }
}
