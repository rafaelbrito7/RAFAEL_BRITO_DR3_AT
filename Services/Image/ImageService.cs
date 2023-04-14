using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Image
{
    public class ImageService
    {

        private ImageRepository Repository { get; set; }

        public ImageService(ImageRepository repository)
        {
            Repository = repository;
        }

        public async Task<string> UploadFile(string type, Guid Id, string fileExtension, byte[] image)
        {
            Stream imageStream = new MemoryStream(image);
            string photoUrl = await Repository.UploadFile(type, Id, fileExtension, imageStream);
            return photoUrl;
        }
    }
}
