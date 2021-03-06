﻿namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedLoggingModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Loggings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        LogDate = c.DateTime(nullable: false),
                        Username = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Loggings", "UserId", "dbo.Users");
            DropIndex("dbo.Loggings", new[] { "UserId" });
            DropTable("dbo.Loggings");
        }
    }
}
