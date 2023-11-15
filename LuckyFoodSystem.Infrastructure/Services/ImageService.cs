using LuckyFoodSystem.AggregationModels.ImageAggregate;
using LuckyFoodSystem.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LuckyFoodSystem.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        public async Task<List<Image>> LoadImage(IFormFileCollection files, string rootPath)
        {
            List<Image> images = new();
            foreach (var file in files)
            {
                Image img = Image.Create(Path.GetExtension(file.FileName));
                using (FileStream fileStream = new(Path.Combine(rootPath, img.Path), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                images.Add(img);
            }
            return images;
        }
        public void RemoveImage(string rootPath, Image img)
        { 
            if (img is not null)
            {
                var oldFile = Path.Combine(rootPath, img.Path);
                if (File.Exists(oldFile))
                {
                    File.Delete(oldFile);
                }
            }
        }       
    }
}
