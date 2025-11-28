using E_Commerce.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Repositries.Services
{
    public class ImageManagementService : IImageManagementService
    {
        private readonly IFileProvider _fileProvider;

        public ImageManagementService(IFileProvider fileProvider)
        { 
            _fileProvider = fileProvider;
        }

        public void DeleteImageasync(string src)
        {
            var info = _fileProvider.GetFileInfo(src);

            var root = info.PhysicalPath;
            File.Delete(root);
        }

        public async Task<List<string>> UploadImageAsync(IFormFileCollection files, string src)
        {
            var saveImageSrcs = new List<string>();
            var imageDirectory = Path.Combine("wwwroot", "Images", src);

            if (!Directory.Exists(imageDirectory))
                Directory.CreateDirectory(imageDirectory);


            foreach(var item in files)
            {
                if (item.Length > 0)
                {
                    var imageName = item.FileName;
                    var imageSrc = $"/Images/{src}/{imageName}";

                    var root = Path.Combine(imageDirectory, imageName);

                    using (FileStream stream = new(root, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }

                    saveImageSrcs.Add(imageSrc);
                }
            }

            return saveImageSrcs;
        }
    }
}
