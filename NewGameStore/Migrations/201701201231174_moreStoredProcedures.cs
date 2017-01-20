namespace NewGameStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moreStoredProcedures : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("dbo.ReturnCopy",
                p => new
                {
                    Copy = p.Int(),
                },
                body:
                    @"UPDATE [dbo].[Copy] 
                        set [Available] = 1 
                        where (CopyID = @Copy)"
                );

            CreateStoredProcedure("dbo.PayFee",
                p => new
                {
                    Fee = p.Int(),
                },
                body:
                    @"UPDATE [dbo].[Fee] 
                        set [Paid] = 1 
                        where (FeeID = @Fee)"
                );
        }
        
        public override void Down()
        {
        }
    }
}
