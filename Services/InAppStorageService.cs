namespace houlala_storage.Services;

public class InAppStorageService : IFileStorageService
{
    private readonly IWebHostEnvironment? _env;
    private readonly IHttpContextAccessor? _httpContextAccessor;

    public InAppStorageService(
        IWebHostEnvironment env,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _env = env;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task DeleteFile(string fileRoute, string containerName)
    {
        var fileName = Path.GetFileName(fileRoute);
        String fileDirectory = Path.Combine(_env!.WebRootPath, containerName, fileName);

        if (File.Exists(fileDirectory))
        {
            File.Delete(fileDirectory);
        }

        return Task.FromResult(0);
    }

    public async Task<string> EditFile(byte[] content, string extension, string containerName, string fileRoute, string contentType)
    {
        if (!String.IsNullOrEmpty(fileRoute))
        {
            await DeleteFile(fileRoute, containerName);
        }
        return await SaveFile(content, extension, containerName, contentType);
    }

    public async Task<string> SaveFile(byte[] content, string extension, string containerName, string contentType)
    {
        var fileName = $"{Guid.NewGuid()}{extension}";
        String folder = Path.Combine(_env!.WebRootPath, containerName);

        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

        String savingPth = Path.Combine(folder, fileName);
        await File.WriteAllBytesAsync(savingPth, content);

        var currentUrl = $"{_httpContextAccessor!.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
        var pathForDatabase = Path.Combine(currentUrl, containerName, fileName).Replace("\\", "/");
        return pathForDatabase;
    }
}