using System.ComponentModel.DataAnnotations;

namespace houlala_storage
{
    public class FileSizeValidator: ValidationAttribute
    {
        private readonly int maxFile; 
        
        public FileSizeValidator(int maxFile)
        {
            this.maxFile = maxFile; 
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext? validationContext)
        {
           if(value == null)
            {
                return ValidationResult.Success; 
            }

            IFormFile? formFile = value as IFormFile; 

            if(formFile == null)
            {
                return ValidationResult.Success; 
            }

            if(formFile.Length > maxFile * 1024 * 1024)
            {
                return new ValidationResult($"File size cannot be bigger than {maxFile} megabytes "); 
            }

            return ValidationResult.Success; 
        }
    }
}