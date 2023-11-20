using LuckyFoodSystem.AggregationModels.ImageAggregate;
using LuckyFoodSystem.Application.Common.Interfaces.Services;
using LuckyFoodSystem.Infrastructure.Persistаnce.Context;
using Microsoft.AspNetCore.Http;

namespace LuckyFoodSystem.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly LuckyFoodDbContext _context;
        public ImageService(LuckyFoodDbContext context) => _context = context;
        public async Task<List<Image>> LoadImages(IFormFileCollection files, string rootPath)
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
            images.ForEach(async img => { await _context.Images.AddAsync(img); });

            return images;
        }

        public void RemoveFromPath(string rootPath, List<Image> images)
        {
            foreach (var img in images)
            {
                var oldFile = Path.Combine(rootPath, img.Path);
                if (File.Exists(oldFile))
                {
                    File.Delete(oldFile);
                }
            }
        }

        public List<Guid> RemoveImages(string rootPath, List<Guid> imageIds)
        {
            List<Guid> imageIdsForDeleting = new();
            foreach (var item in imageIds)
            {
                Image? img = _context.Images.AsEnumerable()
                    .FirstOrDefault(u => u.Id.Value == item);

                if(img is not null)
                {
                    var oldFile = Path.Combine(rootPath, img.Path);
                    if (File.Exists(oldFile))
                    {
                        File.Delete(oldFile);
                    }

                    _context.Images.Remove(img!);
                    imageIdsForDeleting.Add(item);
                }

                continue;
            }

            return imageIdsForDeleting;
        }       
    }
}
