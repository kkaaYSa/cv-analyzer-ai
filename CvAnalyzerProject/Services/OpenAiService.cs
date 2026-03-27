using CvAnalyzerProject.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CvAnalyzerProject.Services
{
    public class OpenAiService : IOpenAiService
    {
        private readonly string _apiKey = "YOUR_API_KEY";
        private readonly string _apiUrl = "https://api.openai.com/v1/chat/completions";

        public async Task<CvAnalysisResultViewModel> AnalyzeCvAsync(string cvText)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

                var prompt = $@"
Aşağıdaki CV metnini analiz et.

Aşağıdaki alanlarda cevap üret:
- GeneralEvaluation
- Strengths
- Weaknesses
- Suggestions
- SuitableRoles
- Score

Kurallar:
- Sadece geçerli JSON döndür.
- Türkçe cevap ver.
- Score 0 ile 100 arasında olsun.
- Strengths, Weaknesses, Suggestions ve SuitableRoles metin olarak dönsün.

CV:
{cvText}";

                var requestBody = new
                {
                    model = "gpt-4o-mini",
                    messages = new object[]
                    {
                        new { role = "system", content = "Sen profesyonel bir CV analiz uzmanısın." },
                        new { role = "user", content = prompt }
                    },
                    temperature = 0.3
                };

                var json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_apiUrl, content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new Exception("OpenAI hatası: " + responseString);

                var parsed = JObject.Parse(responseString);
                var aiText = parsed["choices"]?[0]?["message"]?["content"]?.ToString();

                if (string.IsNullOrWhiteSpace(aiText))
                    throw new Exception("AI cevabı boş geldi.");

                aiText = aiText.Replace("```json", "").Replace("```", "").Trim();

                return JsonConvert.DeserializeObject<CvAnalysisResultViewModel>(aiText);
            }
        }
    }
}