namespace Kalendarz_T_K.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Termins",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Wydarzenies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Notatka = c.String(),
                        Godzina_start = c.String(),
                        Godzina_stop = c.String(),
                        Wykonane = c.Boolean(nullable: false),
                        TerminID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Termins", t => t.TerminID, cascadeDelete: true)
                .Index(t => t.TerminID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wydarzenies", "TerminID", "dbo.Termins");
            DropIndex("dbo.Wydarzenies", new[] { "TerminID" });
            DropTable("dbo.Wydarzenies");
            DropTable("dbo.Termins");
        }
    }
}
