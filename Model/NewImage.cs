using houlala_storage.Validations;
using static houlala_storage.Validations.ContentTypeValidator;

namespace houlala_storage.Model;

public class NewImage
{
    [FileSizeValidator(10)]
    [ContentTypeValidator(ContentTypeGroup.Image)]
    public IFormFile? Image { get; set; }

}