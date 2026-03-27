using System.Threading.Tasks;
using CvAnalyzerProject.ViewModels;

namespace CvAnalyzerProject.Services
{
    public interface IOpenAiService
    {
        Task<CvAnalysisResultViewModel> AnalyzeCvAsync(string cvText);
    }
}