using LuckyFoodSystem.AggregationModels.ImageAggregate;
using Microsoft.AspNetCore.Http;

namespace LuckyFoodSystem.Application.Common.Interfaces.Services
{
    public interface IImageService
    {
        Task<List<Image>> LoadImages(IFormFileCollection files, string rootPath);
        void RemoveImage(string rootPath, List<Image> images);
    }
}
