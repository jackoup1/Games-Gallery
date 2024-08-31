using System.ComponentModel.DataAnnotations;
using GamingWebsite.Attributes;
using GamingWebsite.Settings;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace GamingWebsite.ViewModels {
    public class CreateGameVM :GameVM_Base{
       
        [AllowedExtentions(FileSettings.AllowedExtentions),
            MaxFileSize(FileSettings.MaximumSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;
    }
}
