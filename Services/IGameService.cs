namespace GamingWebsite.Servics {
	public interface IGameService {
		List<Game>? GetGames();
		Task CreateGame(CreateGameVM vm);
		Task<Game?> EditGame(EditGameVM vm);
		bool Delete(int id);
	}
}
