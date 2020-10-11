namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class madeBranchIdInUserNullable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "BranchId", "dbo.Branches");
            DropIndex("dbo.Users", new[] { "BranchId" });
            AlterColumn("dbo.Users", "BranchId", c => c.Int());
            CreateIndex("dbo.Users", "BranchId");
            AddForeignKey("dbo.Users", "BranchId", "dbo.Branches", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "BranchId", "dbo.Branches");
            DropIndex("dbo.Users", new[] { "BranchId" });
            AlterColumn("dbo.Users", "BranchId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "BranchId");
            AddForeignKey("dbo.Users", "BranchId", "dbo.Branches", "Id", cascadeDelete: true);
        }
    }
}
