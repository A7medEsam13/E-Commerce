using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Services
{
    public interface IImageManagementService
    {
        Task<List<string>> UploadImageAsync(IFormFileCollection files, string src);
        void DeleteImageasync(string src);
    }
}
