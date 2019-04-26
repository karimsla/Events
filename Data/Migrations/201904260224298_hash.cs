namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hash : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "password", c => c.String(nullable: false, maxLength: 20));
        }
    }
}