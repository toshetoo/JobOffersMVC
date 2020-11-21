using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobOffersMVC.Services.Abstractions
{
    public interface IFileHelperService
    {
        string BuildFilePath(string directory, string filePath);
        void CreateFile(IFormFile file, string filePath);
    }
}
