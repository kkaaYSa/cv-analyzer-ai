using CvAnalyzerProject.Entities;
using System.Collections.Generic;
using System.Data.Entity;

namespace CvAnalyzerProject.DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("CvAnalyzerDb")
        {
        }

        public DbSet<CvAnalysis> CvAnalyses { get; set; }
    }
}