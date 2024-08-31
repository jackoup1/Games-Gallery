
namespace GamingWebsite.Servics
{
	public class CategoriesService : ICategoriesService {
		private readonly Data.AppContext _context;
		public CategoriesService(Data.AppContext context) {
			_context = context; 
		}
		public IEnumerable<SelectListItem> GetCategories() {
			return _context.Categories.Select(c => new SelectListItem
			{
				Value = c.Id.ToString(),
				Text = c.Name
			}).OrderBy(C => C.Text);
		}
	}
}
