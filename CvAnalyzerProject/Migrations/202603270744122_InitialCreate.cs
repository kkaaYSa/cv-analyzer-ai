namespace CvAnalyzerProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CvAnalysis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CvText = c.String(),
                        GeneralEvaluation = c.String(),
                        Strengths = c.String(),
                        Weaknesses = c.String(),
                        Suggestions = c.String(),
                        SuitableRoles = c.String(),
                        Score = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CvAnalysis");
        }
    }
}
