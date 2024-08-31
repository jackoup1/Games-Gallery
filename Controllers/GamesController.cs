
using GamingWebsite.Servics;


namespace GamingWebsite.Controllers;

public class GamesController : Controller {

    private readonly ICategoriesService _categoriesService;
    private readonly IPlatformService _platformService;
    private readonly IGameService _gameService;
    public GamesController(Data.AppContext context, ICategoriesService categoriesService, IPlatformService platformService, IGameService gameService) {
        _categoriesService = categoriesService;
        _platformService = platformService;
        _gameService = gameService;
    }
    public IActionResult Index() {
        List<Game>? games = _gameService.GetGames()?.OrderBy(g => g.Name).ToList();
        return View(games);
    }


    public IActionResult Details(int id) {
        Game? game = _gameService.GetGames()?.Where(g => g.Id == id).FirstOrDefault();
        if (game is null) {
            return NotFound();
        }
        return View(game);
    }


    public IActionResult Create() {
        CreateGameVM vm = new()
        {
            Categories = _categoriesService.GetCategories(),

            Platforms = _platformService.GetPlatforms()
        };
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateGameVM vm) {

        if (!ModelState.IsValid) {
            vm.Categories = _categoriesService.GetCategories();
            vm.Platforms = _platformService.GetPlatforms();
            return View(vm);
        }

        await _gameService.CreateGame(vm);

        return RedirectToAction(nameof(Index));
    }


    public IActionResult Edit(int id) {
        Game? game = _gameService.GetGames()?.Where(game => game.Id == id).FirstOrDefault();
        if (game != null) {
            EditGameVM vm = new EditGameVM
            {
                id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryID = game.categoryID,
                Categories = _categoriesService.GetCategories(),
                Platforms = _platformService.GetPlatforms(),
                SelectedPlatforms = game.Platforms.Select(p => p.Id).ToList(),
                CoverName = game.Cover
            };
            return View(vm);
        }
        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task <IActionResult >Edit(EditGameVM vm) {
        if (!ModelState.IsValid) {
            vm.Categories = _categoriesService.GetCategories().ToList();
            vm.Platforms = _platformService.GetPlatforms().ToList();
            return View(vm);
        }

        Game? game = await _gameService.EditGame(vm);
        //if an issue happened while updating the game 
        if (game is null) {
            return BadRequest();
        }


        return RedirectToAction(nameof(Index));
    }



    public IActionResult Delete(int id) {
        bool isDeleted = _gameService.Delete(id);
        if (isDeleted) { 
           return RedirectToAction(nameof(Index));
        }
        else {
            return BadRequest();
        }
    }

}


    


