using Microsoft.AspNetCore.Http;
using MVCAPP.Infrastructure.Abstractions;

namespace MVCAPP.Infrastructure.Services;

public class FileManager : IFileManager
{
    public async Task<string> UploadFile(IFormFile file)
    {
        string imageName = @$"{Guid.NewGuid().ToString()}{file.FileName}";

        string path = Path.Combine("wwwroot", "images", imageName);

        using (var fs = new FileStream(path, FileMode.OpenOrCreate))
        {
            await file.CopyToAsync(fs);
        }

        return imageName;
    }

    public async Task DeleteFile(string fileName)
    {
        string path = Path.Combine("wwwroot", "images", fileName);

        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
