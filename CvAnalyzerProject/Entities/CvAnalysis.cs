using System;

namespace CvAnalyzerProject.Entities
{
    public class CvAnalysis
    {
        public int Id { get; set; }
        public string CvText { get; set; }
        public string GeneralEvaluation { get; set; }
        public string Strengths { get; set; }
        public string Weaknesses { get; set; }
        public string Suggestions { get; set; }
        public string SuitableRoles { get; set; }
        public int Score { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}