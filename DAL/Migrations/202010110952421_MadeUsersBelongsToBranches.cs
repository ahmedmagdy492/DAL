namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeUsersBelongsToBranches : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "BranchId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "BranchId");
            AddForeignKey("dbo.Users", "BranchId", "dbo.Branches", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "BranchId", "dbo.Branches");
            DropIndex("dbo.Users", new[] { "BranchId" });
            DropColumn("dbo.Users", "BranchId");
        }
    }
}
