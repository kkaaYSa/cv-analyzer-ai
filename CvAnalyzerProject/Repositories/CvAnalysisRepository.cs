using System.Collections.Generic;
using System.Linq;
using CvAnalyzerProject.DataAccess;
using CvAnalyzerProject.Entities;

namespace CvAnalyzerProject.Repositories
{
    public class CvAnalysisRepository : ICvAnalysisRepository
    {
        private readonly AppDbContext _context;

        public CvAnalysisRepository()
        {
            _context = new AppDbContext();
        }

        public void Add(CvAnalysis entity)
        {
            _context.CvAnalyses.Add(entity);
        }

        public List<CvAnalysis> GetAll()
        {
            return _context.CvAnalyses
                .OrderByDescending(x => x.CreatedDate)
                .ToList();
        }

        public CvAnalysis GetById(int id)
        {
            return _context.CvAnalyses.FirstOrDefault(x => x.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}