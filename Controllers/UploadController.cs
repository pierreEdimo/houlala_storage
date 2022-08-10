using Microsoft.AspNetCore.Mvc;
using houlala_storage.Services; 
using houlala_storage.Model; 

namespace houlala_storage.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly IFileStorageService? _fileStorageService;
        private String containerName = "Images";

        public UploadController(IFileStorageService fileStorageService)
        {
            _fileStorageService = fileStorageService;

        }
        [HttpPost]
        public async Task<ActionResult<String>> uploadImage([FromForm] NewImage NewImage)
        {
            String? imageUrl;

            using (var memoryStream = new MemoryStream())
            {
                await NewImage!.Image!.CopyToAsync(memoryStream);
                var content = memoryStream.ToArray();
                var extension = Path.GetExtension(NewImage!.Image.FileName);
                imageUrl = await _fileStorageService!.SaveFile(content, extension, containerName, NewImage!.Image.ContentType);
            }

            return imageUrl;
        }

        [HttpPut]
        public async Task<ActionResult<String>> editImage([FromForm] EditImage EditImage)
        {
            String? ImageUrl;

            using (var memoryStream = new MemoryStream())
            {
                await EditImage!.Image!.CopyToAsync(memoryStream);
                var content = memoryStream.ToArray();
                var extension = Path.GetExtension(EditImage.Image.FileName);
                ImageUrl = await _fileStorageService!.EditFile(content, extension, containerName, EditImage.ImageUrl!, EditImage.Image.ContentType);
            }

            return ImageUrl;
        }
}