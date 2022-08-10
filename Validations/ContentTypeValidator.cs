using System.ComponentModel.DataAnnotations;

namespace houlala_storage.Validations
{
    public class ContentTypeValidator : ValidationAttribute
    {
        private readonly String[]? validContentTypes;

        private readonly String[]? imageContentTypes = new string[] { "image/jpeg", "image/png", "image/gif" };

        public ContentTypeValidator(String[] ValidContentTypes)
        {
            validContentTypes = ValidContentTypes;
        }

        public ContentTypeValidator(ContentTypeGroup contentTypeGroup)
        {
            switch (contentTypeGroup)
            {
                case ContentTypeGroup.Image:
                    validContentTypes = imageContentTypes;
                    break;
            }
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


            if (!validContentTypes!.Contains(formFile.ContentType))
            {
                return new ValidationResult($"Content-Type should be one the following: {string.Join(", ", validContentTypes!)}");
            }

            return ValidationResult.Success;
        }

        public enum ContentTypeGroup
        {
            Image
        }
    }
}