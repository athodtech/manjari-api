using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace AthodBeTrackApi.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploadService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<string> SaveFile(string folderPath, IFormFile file, string uniqueFName)
        {
            if (!Directory.Exists(Path.Combine(_webHostEnvironment.WebRootPath, folderPath)))
                Directory.CreateDirectory(Path.Combine(_webHostEnvironment.WebRootPath, folderPath));

            var uploadedFilePath = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            return await UploadFile(uploadedFilePath, file, uniqueFName);
        }

        private async Task<string> UploadFile(string uploadedFilePath, IFormFile file, string uniqueFName)
        {
            try
            {
                using (FileStream stream = new FileStream(Path.Combine(uploadedFilePath, uniqueFName), FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    return file.FileName;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

    }
}
