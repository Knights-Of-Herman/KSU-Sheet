using KnightsOfHerman.Backend.Common.Database.Interfaces.Sanctum.Character;
using KnightsOfHerman.Backend.Common.Sanctum.Character.Interfaces;
using KnightsOfHerman.Backend.Common.Sanctum.Character.Services;
using KnightsOfHerman.Backend.Database.Azure.Character;
using KnightsOfHerman.Backend.Database.Testing.UnitTesting;
using KnightsOfHerman.Backend.Server.Memory;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.XUnitTesting.Backend
{
    internal class CharacterCacheTests
    {

        CharacterCache GetCharacterCache()
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            services.AddScoped<ICharacterDB>(sp => new CharacterDB(DBFactory.GetContext()));
            var locks = new CharacterLockService();
            services.AddSingleton<ICharacterLockService>(locks);
            var serviceProvider = services.BuildServiceProvider();
            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            return new CharacterCache(memoryCache, serviceProvider, locks);
        }



    }
}
