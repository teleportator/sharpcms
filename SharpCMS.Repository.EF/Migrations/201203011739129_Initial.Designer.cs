// <auto-generated />
namespace SharpCMS.Repository.EF.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    
    public sealed partial class Initial : IMigrationMetadata
    {
        string IMigrationMetadata.Id
        {
            get { return "201203011739129_Initial"; }
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
