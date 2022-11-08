using System.ComponentModel.DataAnnotations;

namespace houlala_storage.Validations
{
    public class ContentTypeValidator : ValidationAttribute
    {
        private readonly String[]? _validContentTypes;

        private readonly String[]? _imageContentTypes = new string[] { "image/jpeg","image/jpg", "image/png", "image/gif" };

        public ContentTypeValidator(String[] validContentTypes)
        {
            _validContentTypes = validContentTypes;
        }

        public ContentTypeValidator(ContentTypeGroup contentTypeGroup)
        {
            switch (contentTypeGroup)
            {
                case ContentTypeGroup.Image:
                    _validContentTypes = _imageContentTypes;
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


            if (!_validContentTypes!.Contains(formFile.ContentType))
            {
                return new ValidationResult($"Content-Type should be one the following: {string.Join(", ", _validContentTypes!)}");
            }

            return ValidationResult.Success;
        }

        public enum ContentTypeGroup
        {
            Image
        }
    }
}