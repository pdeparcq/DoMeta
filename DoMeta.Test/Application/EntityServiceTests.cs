using System;
using System.Linq;
using System.Threading.Tasks;
using DoMeta.Application;
using DoMeta.Application.Commands;
using DoMeta.Infrastructure;
using Kledex.Extensions;
using Kledex.Store.EF.InMemory.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DoMeta.Test.Application
{
    [TestFixture]
    public class EntityServiceTests
    {
        [Test]
        public async Task CanRegisterEntity()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddKledex(typeof(RegisterEntity)).AddInMemoryStore();
            serviceCollection.AddDbContext<MetaDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            serviceCollection.AddSingleton<EntityService>();
            serviceCollection.AddSingleton<EntityQueryService>();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var service = serviceProvider.GetService<EntityService>();
            var queryService = serviceProvider.GetService<EntityQueryService>();
            var boundedContextId = Guid.NewGuid();

            await service.Register(boundedContextId, "Touchpoint");
            var entity = (await queryService.GetEntities(boundedContextId)).First();
            
            Assert.IsNotNull(entity);
            Assert.AreEqual("Touchpoint", entity.Name);
        }
    }
}
