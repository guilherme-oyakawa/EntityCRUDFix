namespace NewGameStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidClient : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Client", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Client", "Active");
        }
    }
}
