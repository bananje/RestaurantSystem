using LuckyFoodSystem.AggregationModels.ImageAggregate;
using Microsoft.AspNetCore.Http;

namespace LuckyFoodSystem.Application.Common.Interfaces.Services
{
    public interface IImageService
    {
        Task<List<Image>> LoadImage(IFormFileCollection files, string rootPath);
        void RemoveImage(string rootPath, Image img);
    }
}
