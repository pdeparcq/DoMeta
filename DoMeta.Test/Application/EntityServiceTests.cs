using System;
using System.Threading.Tasks;
using DoMeta.Application;
using DoMeta.Application.Commands;
using Kledex.Extensions;
using Kledex.Store.EF.InMemory.Extensions;
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
            serviceCollection.AddSingleton<EntityService>();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var service = serviceProvider.GetService<EntityService>();
            var entity = await service.Register(Guid.NewGuid(), "Touchpoint");

            Assert.NotNull(entity);
            Assert.AreEqual("Id", entity.Identity.Name);
        }
    }
}
