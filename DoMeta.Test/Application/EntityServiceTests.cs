using System;
using System.Linq;
using System.Threading.Tasks;
using DoMeta.Application.Commands;
using DoMeta.Application.Queries;
using DoMeta.Domain;
using DoMeta.Domain.ValueObjects;
using NUnit.Framework;

namespace DoMeta.Test.Application
{
    [TestFixture]
    public class EntityServiceTests : IntegrationTestBase
    {
        [Test]
        public async Task CanRegisterEntity()
        {
            // Commands
            var entity = await Dispatcher.SendAsync<Entity>(new RegisterEntity
            {
                BoundedContextId = Guid.NewGuid(),
                Name = "Touchpoint"
            });

            entity = await Dispatcher.SendAsync<Entity>(new AddPropertyToEntity()
            {
                AggregateRootId = entity.Id,
                Property = new Property("Name", typeof(string))
            });

            entity = await Dispatcher.SendAsync<Entity>(new AddPropertyToEntity()
            {
                AggregateRootId = entity.Id,
                Property = new Property("Parent", entity.Id)
            });

            // Query
            var entityData = (await Dispatcher.GetResultAsync(new GetEntities
            {
                BoundedContextId = entity.BoundedContextId
            })).First();
            
            // Verify
            Assert.IsNotNull(entityData);
            Assert.AreEqual(entity.BoundedContextId, entityData.BoundedContextId);
            Assert.AreEqual(entity.Id, entityData.MetaTypeId);
            Assert.AreEqual(entity.Name, entityData.Name);
            Assert.IsNotEmpty(entityData.Properties);
        }
    }
}
