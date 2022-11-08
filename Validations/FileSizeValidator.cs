using System.ComponentModel.DataAnnotations;

namespace houlala_storage.Validations
{
    public class FileSizeValidator : ValidationAttribute
    {
        private readonly int _maxFile;

        public FileSizeValidator(int maxFile)
        {
            this._maxFile = maxFile;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext? validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile? formFile = value as IFormFile;

            if (formFile == null)
            {
                return ValidationResult.Success;
            }

            if (formFile.Length > _maxFile * 4048 * 4048)
            {
                return new ValidationResult($"File size cannot be bigger than {_maxFile} megabytes ");
            }

            return ValidationResult.Success;
        }
    }
}