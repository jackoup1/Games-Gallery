using System.ComponentModel.DataAnnotations;
using GamingWebsite.Settings;

namespace GamingWebsite.Attributes {
    public class MaxFileSizeAttribute : ValidationAttribute {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize) { 
            _maxFileSize = maxFileSize;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
            IFormFile? file = (IFormFile?) value;
            if (file != null) { 
                if(file.Length > _maxFileSize) {
                    return new ValidationResult($"Maximum Allowed Size is {FileSettings.MaximumSizeInMB}MB");
                }
            }
            return ValidationResult.Success;
        }
    }
}
