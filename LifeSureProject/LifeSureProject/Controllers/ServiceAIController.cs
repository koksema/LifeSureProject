using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Text;
using Newtonsoft.Json;
using LifeSureProject.Models.DataModels;
using LifeSureProject.Models.ViewModels;

namespace LifeSureProject.Controllers
{
    public class ServiceAIController : Controller
    {
        private readonly LifeSureDbEntities db = new LifeSureDbEntities();

        public ActionResult Index()
        {
            var values = db.TblService.OrderByDescending(x => x.ServiceId).Take(4).ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult GenerateImage(int? id)
        {
            if (id.HasValue)
            {
                var service = db.TblService.FirstOrDefault(x => x.ServiceId == id.Value);
                if (service != null)
                {
                    return View(new ServicesAIViewModel
                    {
                        ServiceId = service.ServiceId,
                        ServicesCardTitle = service.ServiceTitle,     
                        ServicesCardIcon = service.ServiceIcon,          
                        ServicesCardImageUrl = service.ServiceImg       
                    });
                }
            }

            return View(new ServicesAIViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GenerateImage(ServicesAIViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.ServicesCardDescription))
            {
                ModelState.AddModelError("ServicesCardDescription", "Lütfen bir açıklama giriniz.");
                return View(model);
            }

            var apiKey = "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"; // 🔐 key'i buraya koyma, ideal: web.config

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMinutes(3);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                var requestBody = new
                {
                    model = "gpt-image-1",
                    prompt = model.ServicesCardDescription,
                    size = "1024x1024"
                };

                var json = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://api.openai.com/v1/images/generations", content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "API Hatası: " + responseString);
                    return View(model);
                }

                // ✅ Güvenli parse (dynamic yerine)
                var imageResult = JsonConvert.DeserializeObject<ImageResponse>(responseString);
                var base64Image = imageResult?.data?.FirstOrDefault()?.b64_json;

                if (string.IsNullOrWhiteSpace(base64Image))
                {
                    ModelState.AddModelError("", "API yanıtında görsel bulunamadı: " + responseString);
                    return View(model);
                }

                byte[] imageBytes = Convert.FromBase64String(base64Image);

                var fileName = Guid.NewGuid() + ".png";
                var relativePath = "/Content/AiImages/" + fileName;
                var serverPath = Server.MapPath(relativePath);

                var dir = Path.GetDirectoryName(serverPath);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                System.IO.File.WriteAllBytes(serverPath, imageBytes);

                // View önizlemesi için
                model.ServicesCardImageUrl = relativePath;

                // ✅ DB'ye kaydet (INSERT veya UPDATE)
                TblService service;

                if (model.ServiceId.HasValue && model.ServiceId.Value > 0)
                {
                    // UPDATE
                    service = db.TblService.FirstOrDefault(x => x.ServiceId == model.ServiceId.Value);
                    if (service == null)
                    {
                        ModelState.AddModelError("", "Hizmet bulunamadı. Kaydedilemedi.");
                        return View(model);
                    }
                }
                else
                {
                    // INSERT
                    service = new TblService();
                    db.TblService.Add(service);
                }

                // ⚠️ BURADAKİ KOLON ADLARINI KENDİ TABLONA GÖRE DÜZELT
                service.ServiceTitle = model.ServicesCardTitle;     // örnek kolon
                service.ServiceIcon = model.ServicesCardIcon;         // örnek kolon
                service.ServiceImg = relativePath;                    // örnek kolon (resim yolu)

                db.SaveChanges();

                // yeni eklenirse id geri dolduralım
                model.ServiceId = service.ServiceId;

                TempData["Success"] = "Başarıyla üretildi ve veritabanına kaydedildi!";
            }

            return View(model);
        }

        // ✅ Response modelleri
        public class ImageResponse
        {
            public System.Collections.Generic.List<ImageData> data { get; set; }
        }

        public class ImageData
        {
            public string b64_json { get; set; }
        }
    }
}