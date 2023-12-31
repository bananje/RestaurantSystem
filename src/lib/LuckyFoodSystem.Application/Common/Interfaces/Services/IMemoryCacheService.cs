﻿namespace LuckyFoodSystem.Application.Common.Interfaces.Services
{
    public interface IMemoryCacheService 
    {
        T Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan absoluteExpiration);
        void Remove(string key);
    }
}
