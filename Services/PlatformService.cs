namespace GamingWebsite.Servics {
	public class PlatformService : IPlatformService {
		private readonly Data.AppContext _context;
		public PlatformService(Data.AppContext context) {
			_context = context;
		}
		public IEnumerable<SelectListItem> GetPlatforms() {
			return _context.Platforms.Select(p => new SelectListItem
			{
				Text = p.Name,
				Value = p.Id.ToString()
			})
				.OrderBy(p => p.Text)
				.AsNoTracking();
		}
	}
}
