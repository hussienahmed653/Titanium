using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titanium2.Application.Interfaces.IImageInterface
{
    public interface IImageService
    {
        public Task<string> UplodeImage(IFormFile image, int? sectionid);
    }
}
