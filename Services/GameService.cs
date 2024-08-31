
using GamingWebsite.Settings;

namespace GamingWebsite.Servics {
	public class GameService : IGameService {
		private readonly Data.AppContext _context;
		private readonly IWebHostEnvironment _env;
		private readonly string _imagePath;
		public GameService(Data.AppContext context, IWebHostEnvironment env) {
			_context = context;
			_env = env;
			_imagePath = $"{_env.WebRootPath}{FileSettings.ImagePath}";
		}

		public List<Game>? GetGames() {
			return _context.Games
				.Include(g =>g.Category)
				.Include(p =>p.Platforms)
				.AsNoTracking()?
				.ToList();
		}

		public async Task CreateGame(CreateGameVM vm) {

			var CoverName = $"{Guid.NewGuid()}{Path.GetExtension(vm.Cover.FileName)}";

			var path = Path.Combine(_imagePath, CoverName);

			using var stream = File.Create(path);
			await vm.Cover.CopyToAsync(stream);

            var selectedPlatforms = _context.Platforms
                .Where(p => vm.SelectedPlatforms.Contains(p.Id))
                .ToList();

			Game game = new()
			{
				Name = vm.Name,
				Description = vm.Description,
				categoryID = vm.CategoryID,
				Cover = CoverName,
				Platforms = selectedPlatforms
			};
		
			_context.Add(game);
			await _context.SaveChangesAsync();
		}

        public async Task<Game?> EditGame(EditGameVM vm) {
			
			bool hasNewcover = (vm.Cover != null);
			//converting the Selected Platforms to be Collection of platforms
			var selectedPlatforms = _context.Platforms
				.Where(p => vm.SelectedPlatforms.Contains(p.Id))
				.ToList();

			Game? GameToEdit = _context.Games.Where(g=>g.Id == vm.id)
				.Include(g=>g.Platforms)
				.FirstOrDefault();

			if (GameToEdit is null)
				return null;

			GameToEdit.Name = vm.Name;
			GameToEdit.Description = vm.Description;
			GameToEdit.categoryID = vm.CategoryID;
			GameToEdit.Platforms = selectedPlatforms;
			//Saving the old cover name so i can delete it later
			string OldCover = "";
            if (hasNewcover) 
			{ 
				OldCover = GameToEdit.Cover;
                //adding the new cover to server
                GameToEdit.Cover = await SaveCover(vm.Cover!);
            }
			
            var EffectedRowsCount = _context.SaveChanges();
            //checking if changes where succesfully made to the DataBase
            if (EffectedRowsCount > 0) {
				if (hasNewcover) {
					//Deleting old cover from the server
					string CoverToDelete = $"{_imagePath}{OldCover}";
					File.Delete(CoverToDelete);
				}
				return GameToEdit;
            }
			//in case it wasn't succesfully saved so i want to delete the new cover from server
			else {
                string CoverToDelete = Path.Combine(_imagePath, OldCover);
                File.Delete(CoverToDelete);
				return null;
            }
        }


		public bool Delete(int id) {
			bool isDelete = false;

			Game? GameToDelete = _context.Games.Where(g => g.Id == id).FirstOrDefault();
			if (GameToDelete is null)
				return isDelete;

			_context.Remove(GameToDelete);
			int EffectedRows = _context.SaveChanges();

			if (EffectedRows > 0) {
				isDelete = true;

				string CoverToDelete =Path.Combine(_imagePath,GameToDelete.Cover);
				File.Delete(CoverToDelete );	
			}
			return isDelete;
		}

		private async Task <string> SaveCover(IFormFile cover) {
			string CoverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
			string path = $"{_imagePath}{CoverName}";

			using var stream = File.Create(path);
			await cover.CopyToAsync(stream);

			return CoverName;
		}
    }
}
