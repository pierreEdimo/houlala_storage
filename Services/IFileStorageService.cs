namespace houlala_storage.Services;

public interface IFileStorageService
{
    Task<String> EditFile(byte[] content, String extension, String containerName, String fileRoute, String contentType);
    Task DeleteFile(String fileRoute, String containerName);
    Task<String> SaveFile(byte[] content, String extension, String containerName, String contentType);
}