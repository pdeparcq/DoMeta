using System;
using System.Linq;
using System.Threading.Tasks;
using DoMeta.Application;
using DoMeta.Application.Commands;
using DoMeta.Application.Queries;
using DoMeta.Infrastructure;
using Kledex;
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
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var dispatcher = serviceProvider.GetService<IDispatcher>();
            var boundedContextId = Guid.NewGuid();

            // Command
            await dispatcher.SendAsync(new RegisterEntity
            {
                BoundedContextId = boundedContextId,
                Name = "Touchpoint"
            });

            // Query
            var entity = (await dispatcher.GetResultAsync(new GetEntities
            {
                BoundedContextId = boundedContextId
            })).First();
            
            // Verify
            Assert.IsNotNull(entity);
            Assert.AreEqual("Touchpoint", entity.Name);
        }
    }
}
