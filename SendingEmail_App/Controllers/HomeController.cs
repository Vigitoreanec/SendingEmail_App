using Microsoft.AspNetCore.Mvc;
using SendingEmail_App.Models;
using System.Diagnostics;
using System.Reflection;

namespace SendingEmail_App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Service service;
        
        public HomeController(ILogger<HomeController> logger, Service service)
        {
            _logger = logger;
            this.service = service;
        }
        public ActionResult Index()
        {
            return View(new RegistrationModel());
        }
        [HttpPost]
        public ActionResult Index(RegistrationModel model)
        {

            model.ConfirmationCode = GenerateConfirmationCode();

            try
            {
                service.SendEmailAnswer(model.Email, model.ConfirmationCode);
                ViewBag.Message = "Код подтверждения отправлен на ваш email";
                ViewBag.ShowCode = true; // Флаг для отображения кода на странице
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ошибка при отправке email: " + ex.Message);
                _logger.LogError(ex, "Ошибка отправки email подтверждения");
            }

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private string GenerateConfirmationCode()
        {
            Random random = new Random();
            int codeLength = random.Next(0, 2) == 0 ? 4 : 6;

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; // Только заглавные буквы и цифры
            char[] code = new char[codeLength];

            for (int i = 0; i < codeLength; i++)
            {
                code[i] = chars[random.Next(chars.Length)];
            }

            return new string(code);
        }
    }
}
