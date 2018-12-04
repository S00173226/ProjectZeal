namespace TestDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseConfigured : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Device",
                c => new
                    {
                        DeviceId = c.Int(nullable: false, identity: true),
                        macAddress = c.String(),
                        VehicleMake = c.String(),
                        VehicleModel = c.String(),
                        VehicleDesc = c.String(),
                    })
                .PrimaryKey(t => t.DeviceId);
            
            CreateTable(
                "dbo.Geolocations",
                c => new
                    {
                        DeviceID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        DateTimeRecorded = c.DateTime(nullable: false, storeType: "date"),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.DeviceID, t.UserID, t.DateTimeRecorded })
                .ForeignKey("dbo.Device", t => t.DeviceID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.DeviceID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Address3 = c.String(),
                        County = c.String(),
                        ContactNo = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Geolocations", "UserID", "dbo.User");
            DropForeignKey("dbo.Geolocations", "DeviceID", "dbo.Device");
            DropIndex("dbo.Geolocations", new[] { "UserID" });
            DropIndex("dbo.Geolocations", new[] { "DeviceID" });
            DropTable("dbo.User");
            DropTable("dbo.Geolocations");
            DropTable("dbo.Device");
        }
    }
}
