namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedIsOnlineAttributeToBranchModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Branches", "IsOnline", c => c.Boolean(nullable: false));
            DropColumn("dbo.Orders", "IsOnline");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "IsOnline", c => c.Boolean(nullable: false));
            DropColumn("dbo.Branches", "IsOnline");
        }
    }
}
