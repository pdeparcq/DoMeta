using System;
using System.Linq;
using System.Threading.Tasks;
using DoMeta.Application.Commands;
using DoMeta.Application.Queries;
using DoMeta.Domain;
using NUnit.Framework;

namespace DoMeta.Test.Application
{
    [TestFixture]
    public class EntityServiceTests : IntegrationTestBase
    {
        [Test]
        public async Task CanRegisterEntity()
        {
            // Command
            var entity = await Dispatcher.SendAsync<Entity>(new RegisterEntity
            {
                BoundedContextId = Guid.NewGuid(),
                Name = "Touchpoint"
            });

            // Query
            var entityData = (await Dispatcher.GetResultAsync(new GetEntities
            {
                BoundedContextId = entity.BoundedContextId
            })).First();
            
            // Verify
            Assert.IsNotNull(entityData);
            Assert.AreEqual(entity.Name, entityData.Name);
        }
    }
}
