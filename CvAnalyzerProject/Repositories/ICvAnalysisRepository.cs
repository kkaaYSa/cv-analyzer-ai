using System.Collections.Generic;
using CvAnalyzerProject.Entities;

namespace CvAnalyzerProject.Repositories
{
    public interface ICvAnalysisRepository
    {
        void Add(CvAnalysis entity);
        List<CvAnalysis> GetAll();
        CvAnalysis GetById(int id);
        void Save();
    }
}