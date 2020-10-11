namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedIsCustomerAttrToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsCustomer", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsCustomer");
        }
    }
}
