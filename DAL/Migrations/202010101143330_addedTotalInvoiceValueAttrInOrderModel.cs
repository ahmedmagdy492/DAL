namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedTotalInvoiceValueAttrInOrderModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "TotalInvoiceValue", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "TotalInvoiceValue");
        }
    }
}
