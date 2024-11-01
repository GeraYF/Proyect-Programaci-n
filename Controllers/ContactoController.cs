using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Trabajo_Final.Data;
using Trabajo_Final.Helper;
using Trabajo_Final.Models;
using SentimentAnalysis;

namespace Trabajo_Final.Controllers
{
    public class ContactoController : Controller
    {
        private readonly ILogger<ContactoController> _logger;
        private readonly ApplicationDbContext _context;

        public ContactoController(ILogger<ContactoController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Enviar(Contacto contacto)
        {
            _logger.LogDebug("Ingreso a enviar mensaje");

            MLModelSentimentAnalysis.ModelInput sampleData = new MLModelSentimentAnalysis.ModelInput()
            {
                Comentario = contacto.Message
            };
            var sortedScoresWithLabel = MLModelSentimentAnalysis.PredictAllLabels(sampleData);
            foreach (var score in sortedScoresWithLabel)
            {
                Console.WriteLine($"{score.Key,-40}{score.Value,-20}");
            }
            MLModelSentimentAnalysis.ModelOutput output = MLModelSentimentAnalysis.Predict(sampleData);
            Console.WriteLine($"{output.Label}{output.PredictedLabel}");
            output.Score.ToList().ForEach(score => Console.WriteLine(score));

            _context.Add(contacto);
            _context.SaveChanges();
            TempData["Confirm"] = true;
            var emailService2 = new SendMailSendGrid();
            await emailService2.EnviarCorreoAsync(contacto.Nombre, contacto.Email, "Atención Infocom Technology", contacto.Message);
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}