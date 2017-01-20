namespace NewGameStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActivateClientProcedure : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("dbo.ActivateClient",
                p => new
                {
                    Client = p.Int(),
                },
                body:
                    @"UPDATE [dbo].[Client] 
                        set [Active] = 1 
                        where (ClientID = @Client)"
                );
        }

        public override void Down()
        {
        }
    }
}
