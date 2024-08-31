using System.ComponentModel.DataAnnotations;

namespace GamingWebsite.ViewModels {
	public class GameVM_Base {
		[MaxLength(250)]
		public string Name { get; set; } = String.Empty;

		[Display(Name = "Category")]
		public int CategoryID { get; set; }
		public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();

		[Display(Name = "Supported Platforms")]
		public List<int> SelectedPlatforms { get; set; } = default!;

		public IEnumerable<SelectListItem> Platforms { get; set; } = Enumerable.Empty<SelectListItem>();

		[MaxLength(2500)]
		public string Description { get; set; } = String.Empty;
	}
}
