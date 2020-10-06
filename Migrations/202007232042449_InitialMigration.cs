namespace PrisonDatabase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Equipments",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    FreeSlots = c.Int(nullable: false),
                    Prison_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Prisons", t => t.Prison_Id)
                .Index(t => t.Prison_Id);

            CreateTable(
                "dbo.Prisons",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Address = c.String(),
                    City = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.PrisonBlocks",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                    Prison_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Prisons", t => t.Prison_Id)
                .Index(t => t.Prison_Id);

            CreateTable(
                "dbo.Prisoners",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    FirstName = c.String(),
                    Surname = c.String(),
                    CrimeType = c.Int(nullable: false),
                    PrisonBlock_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PrisonBlocks", t => t.PrisonBlock_Id)
                .Index(t => t.PrisonBlock_Id);

            CreateTable(
                "dbo.PrisonerActions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(),
                    Value = c.Boolean(nullable: false),
                    Prisoner_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Prisoners", t => t.Prisoner_Id)
                .Index(t => t.Prisoner_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.PrisonerActions", "Prisoner_Id", "dbo.Prisoners");
            DropForeignKey("dbo.Prisoners", "PrisonBlock_Id", "dbo.PrisonBlocks");
            DropForeignKey("dbo.PrisonBlocks", "Prison_Id", "dbo.Prisons");
            DropForeignKey("dbo.Equipments", "Prison_Id", "dbo.Prisons");
            DropIndex("dbo.PrisonerActions", new[] { "Prisoner_Id" });
            DropIndex("dbo.Prisoners", new[] { "PrisonBlock_Id" });
            DropIndex("dbo.PrisonBlocks", new[] { "Prison_Id" });
            DropIndex("dbo.Equipments", new[] { "Prison_Id" });
            DropTable("dbo.PrisonerActions");
            DropTable("dbo.Prisoners");
            DropTable("dbo.PrisonBlocks");
            DropTable("dbo.Prisons");
            DropTable("dbo.Equipments");
        }
    }
}
