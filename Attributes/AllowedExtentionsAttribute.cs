using System.ComponentModel.DataAnnotations;

namespace GamingWebsite.Attributes {
    public class AllowedExtentionsAttribute : ValidationAttribute {
        private readonly string _allowedExtentions;


        public AllowedExtentionsAttribute(string allowedExtentions) {
            _allowedExtentions = allowedExtentions;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext ctx) {
            IFormFile? file = (IFormFile?)value;
            if (file != null) {
                string extention = Path.GetExtension(file.FileName);
                bool IsAllowed = _allowedExtentions.Split(",").Contains(extention);

                if (!IsAllowed) {
                    return new ValidationResult($"Only {_allowedExtentions.Replace(",", " ")} are allowed");
                }
            }

            return ValidationResult.Success;
        }
    }
}
