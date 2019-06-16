using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace PersonalEconomist.Services.Services.FileService
{
    public class FileService : IFileService
    {
        private IHostingEnvironment _environment;

        public FileService(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public void UpdateFile(string oldFileName, IFormFile formFile, string fileName)
        {
            DeleteFile(oldFileName);
            SaveFile(formFile, fileName);
        }

        public void SaveFile(IFormFile formFile, string fileName)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "images");
            var fullPath = Path.Combine(uploads, fileName);
            formFile.CopyTo(new FileStream(fullPath, FileMode.Create));
        }

        public void DeleteFile(string fileName)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "images");
            File.Delete(Path.Combine(uploads, fileName));
        }

        public string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
    }
}
