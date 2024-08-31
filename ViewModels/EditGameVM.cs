using GamingWebsite.Attributes;
using GamingWebsite.Settings;

namespace GamingWebsite.ViewModels {
	public class EditGameVM : GameVM_Base{
		public int id {get; set; }
		public string CoverName { get; set; } = string.Empty;
		[AllowedExtentions(FileSettings.AllowedExtentions),
			MaxFileSize(FileSettings.MaximumSizeInBytes)]
		public IFormFile? Cover { get; set; } = default!;
    }
}
