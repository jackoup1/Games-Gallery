using System.Diagnostics;
using GamingWebsite.Models;
using GamingWebsite.Servics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GamingWebsite.Controllers {
    public class HomeController : Controller {
        private readonly IGameService _gameService;

        public HomeController(IGameService gameService) {
            _gameService = gameService;
        }

        public IActionResult Index() =>View(_gameService.GetGames());
        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
