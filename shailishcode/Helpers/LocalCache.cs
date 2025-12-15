using System;
using Microsoft.Extensions.Caching.Memory;

namespace AdvanceCRM.Web.Helpers
{
    public static class LocalCache
    {
        private static readonly IMemoryCache _memory = new MemoryCache(new MemoryCacheOptions());

        public static TItem GetLocalStoreOnly<TItem>(string key, TimeSpan expiration, string generationKey, Func<TItem> factory)
        {
            var cacheKey = key + ":" + generationKey;
            return _memory.GetOrCreate(cacheKey, entry =>
            {
                if (expiration > TimeSpan.Zero)
                    entry.AbsoluteExpirationRelativeToNow = expiration;
                return factory();
            });
        }

        public static TItem Get<TItem>(string key, TimeSpan expiration, Func<TItem> factory)
        {
            return _memory.GetOrCreate(key, entry =>
            {
                if (expiration > TimeSpan.Zero)
                    entry.AbsoluteExpirationRelativeToNow = expiration;
                return factory();
            });
        }

        public static void Remove(string key)
        {
            _memory.Remove(key);
        }

        public static void ExpireGroupItems(string generationKey)
        {
            // simplistic implementation clears all cached items
            (_memory as MemoryCache)?.Compact(1.0);
        }
    }
}
