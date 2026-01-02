using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AthodBeTrackApi.Services
{
    public interface IFileUploadService
    {
        Task<string> SaveFile(string folderPath, IFormFile file, string uniqueFName);
    }
}