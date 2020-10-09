namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRoleFormsModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FormViews", "RoleId", "dbo.Roles");
            DropIndex("dbo.FormViews", new[] { "RoleId" });
            RenameColumn(table: "dbo.FormViews", name: "RoleId", newName: "Role_Id");
            CreateTable(
                "dbo.RoleForms",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        FormId = c.Int(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FormViews", t => t.FormId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.FormId);
            
            AlterColumn("dbo.FormViews", "Role_Id", c => c.Int());
            CreateIndex("dbo.FormViews", "Role_Id");
            AddForeignKey("dbo.FormViews", "Role_Id", "dbo.Roles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FormViews", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.RoleForms", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.RoleForms", "FormId", "dbo.FormViews");
            DropIndex("dbo.RoleForms", new[] { "FormId" });
            DropIndex("dbo.RoleForms", new[] { "RoleId" });
            DropIndex("dbo.FormViews", new[] { "Role_Id" });
            AlterColumn("dbo.FormViews", "Role_Id", c => c.Int(nullable: false));
            DropTable("dbo.RoleForms");
            RenameColumn(table: "dbo.FormViews", name: "Role_Id", newName: "RoleId");
            CreateIndex("dbo.FormViews", "RoleId");
            AddForeignKey("dbo.FormViews", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
        }
    }
}
