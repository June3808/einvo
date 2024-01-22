using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyAbp.FileManagement.Storage
{
    public class TempFileCacheManager : ITempFileCacheManager
    {
        public const string TempFileCacheName = "TempFileCacheName";

        private readonly IDistributedCache _cacheManager;

        public TempFileCacheManager(IDistributedCache cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public void SetFile(string token, byte[] content)
        {
            _cacheManager.Set(token, content, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = new TimeSpan(0, 0, 1, 0) // expire time is 1 min by default
            });
        }

        public byte[] GetFile(string token)
        {
            return _cacheManager.Get(TempFileCacheName) as byte[];
        }
    }
}
