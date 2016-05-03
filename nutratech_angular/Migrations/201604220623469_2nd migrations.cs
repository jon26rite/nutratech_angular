namespace vikaro_angular.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2ndmigrations : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AuditUser = c.String(),
                        AuditDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Journals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TransactionDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        DebitCreditMode = c.String(),
                        Amount = c.Double(nullable: false),
                        AuditUser = c.String(),
                        AuditDate = c.DateTime(nullable: false),
                        Account_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .Index(t => t.Account_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Journals", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.Journals", new[] { "Account_Id" });
            DropTable("dbo.Journals");
            DropTable("dbo.Accounts");
        }
    }
}
