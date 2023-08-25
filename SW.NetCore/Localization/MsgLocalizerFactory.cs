using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SW.NetCore.Localization
{
    public class MsgLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IDistributedCache _cache;
        public MsgLocalizerFactory(IDistributedCache cache)
        {
            _cache = cache;
        }
        public IStringLocalizer Create(Type resourceSource) =>
            new MsgLocalizerHelper(_cache);
        public IStringLocalizer Create(string baseName, string location) =>
            new MsgLocalizerHelper(_cache);
    }
}
