using Microsoft.AspNetCore.Http;

namespace MVCAPP.Infrastructure.Abstractions;

public interface IFileManager
{
    Task<string> UploadFile(IFormFile file);
    Task DeleteFile(string fileName);
}