using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TrabukaApi.Helpers
{
    public static class FileUploadHelper
    {
        public static async Task<string> SaveImageAsync(IFormFile file, string subfolder = "")
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Arquivo de imagem inválido.");

            var folderPath = Path.Combine("wwwroot", "assets", "images", subfolder);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Retorna apenas o nome do arquivo para uso na API
            return fileName;
        }
    }
} 