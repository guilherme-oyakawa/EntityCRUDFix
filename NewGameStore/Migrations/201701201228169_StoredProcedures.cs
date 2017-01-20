namespace NewGameStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StoredProcedures : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("dbo.DeactivateClient",
                p => new
                {
                    Client = p.Int(),
                },
                body:
                    @"UPDATE [dbo].[Client] 
                        set [Active] = 0 
                        where (ClientID = @Client)"
                );
        }
        
        public override void Down()
        {
        }
    }
}
