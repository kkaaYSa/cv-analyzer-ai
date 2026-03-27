using CvAnalyzerProject.Entities;
using CvAnalyzerProject.Repositories;
using CvAnalyzerProject.Services;
using CvAnalyzerProject.ViewModels;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;

namespace CvAnalyzerProject.Controllers
{
    public class CvController : Controller
    {
        private readonly ICvAnalysisRepository _cvAnalysisRepository;
        private readonly IOpenAiService _openAiService;
        private readonly PdfService _pdfService;

        public CvController()
        {
            _cvAnalysisRepository = new CvAnalysisRepository();
            _openAiService = new OpenAiService();
            _pdfService = new PdfService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Analyze(CvAnalysisRequestViewModel model, HttpPostedFileBase pdfFile)
        {
            string cvText = model.CvText;

            if (pdfFile != null && pdfFile.ContentLength > 0)
            {
                cvText = _pdfService.ExtractText(pdfFile.InputStream);
            }

            if (string.IsNullOrWhiteSpace(cvText))
            {
                ViewBag.Error = "Lütfen CV metni girin veya PDF yükleyin.";
                return View("Index");
            }

            try
            {
                var result = await _openAiService.AnalyzeCvAsync(cvText);

                var entity = new CvAnalysis
                {
                    CvText = cvText,
                    GeneralEvaluation = result.GeneralEvaluation,
                    Strengths = result.Strengths,
                    Weaknesses = result.Weaknesses,
                    Suggestions = result.Suggestions,
                    SuitableRoles = result.SuitableRoles,
                    Score = result.Score,
                    CreatedDate = DateTime.Now
                };

                _cvAnalysisRepository.Add(entity);
                _cvAnalysisRepository.Save();

                return View("Index", entity);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Hata: " + ex.Message;
                return View("Index");
            }
        }
        public ActionResult History()
        {
            var analyses = _cvAnalysisRepository.GetAll();
            return View(analyses);
        }
    }
}