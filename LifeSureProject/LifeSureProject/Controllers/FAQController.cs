using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LifeSureProject.Models.DataModels;
using LifeSureProject.ViewModels;

namespace LifeSureProject.Controllers
{
    public class FAQAIController : Controller
    {
        private readonly LifeSureDbEntities db = new LifeSureDbEntities();

        public ActionResult FAQAIList()
        {
            var faq = db.TblQuestion.OrderByDescending(x => x.QuestionId).Take(10).ToList();
            return View(faq);
        }

        [HttpGet]
        public ActionResult GenerateAI()
        {
            return View(new FAQViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GenerateAI(FAQViewModel model, string submitType)
        {
            if (submitType == "SoruOlustur")
            {
                // API URL'si ile uğraşmak yerine hızlıca simüle ediyoruz
                await Task.Delay(500); // Gerçekçi bir bekleme süresi
                model.Question = $"{model.QuestionTitle} hakkında bilmemiz gereken en önemli detay nedir?";
            }
            else if (submitType == "CevapOlustur")
            {
                await Task.Delay(800);
                model.QuestionAnswer = $"{model.QuestionTitle} kapsamında sunduğumuz hizmetler, müşteri memnuniyeti ve güvenli gelecek odaklıdır. Detaylı bilgi için uzmanlarımızla görüşebilirsiniz.";
            }
            else if (submitType == "VeritabaninaKaydet")
            {
                if (!string.IsNullOrEmpty(model.Question) && !string.IsNullOrEmpty(model.QuestionAnswer))
                {
                    var entity = new TblQuestion
                    {
                        QuestionTitle = model.QuestionTitle,
                        Question = model.Question,
                        QuestionAnswer = model.QuestionAnswer
                    };
                    db.TblQuestion.Add(entity);
                    db.SaveChanges();
                    return RedirectToAction("FAQAIList");
                }
            }

            ModelState.Clear();
            return View("GenerateAI", model);
        }
    }
}