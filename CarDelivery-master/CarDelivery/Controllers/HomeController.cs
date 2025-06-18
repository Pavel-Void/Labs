using System.Diagnostics;
using CarDelivery.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarDelivery.Controllers
{
    /// <summary>
    /// Главный контроллер приложения.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Конструктор главного контроллера.
        /// </summary>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Главная страница.
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Страница политики конфиденциальности.
        /// </summary>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Страница ошибки.
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
