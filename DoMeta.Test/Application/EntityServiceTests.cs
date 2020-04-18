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
            var touchpoint = await Dispatcher.SendAsync<Entity>(new RegisterEntity
            {
                BoundedContextId = Guid.NewGuid(),
                Name = "Touchpoint"
            });

            var participant = await Dispatcher.SendAsync<Entity>(new RegisterEntity
            {
                BoundedContextId = touchpoint.BoundedContextId,
                Name = "Participant"
            });

            touchpoint = await Dispatcher.SendAsync<Entity>(new AddPropertyToEntity()
            {
                AggregateRootId = touchpoint.Id,
                Property = new Property("Name", typeof(string))
            });

            touchpoint = await Dispatcher.SendAsync<Entity>(new AddRelationToEntity()
            {
                AggregateRootId = touchpoint.Id,
                Name = "Participants",
                MetaTypeId = participant.Id,
                Minimum = 0
            });

            // Query
            var touchpointData = (await Dispatcher.GetResultAsync(new GetEntities
            {
                BoundedContextId = touchpoint.BoundedContextId
            })).First();
            
            // Verify
            Assert.IsNotNull(touchpointData);
            Assert.AreEqual(touchpoint.BoundedContextId, touchpointData.BoundedContextId);
            Assert.AreEqual(touchpoint.Id, touchpointData.MetaTypeId);
            Assert.AreEqual(touchpoint.Name, touchpointData.Name);
            Assert.IsNotNull(touchpointData.Identity);
            Assert.AreEqual("Id", touchpointData.Identity.Name);
            Assert.AreEqual("System.Guid", touchpointData.Identity.SystemType);
            Assert.IsNotEmpty(touchpointData.Properties);
            Assert.AreEqual("Name", touchpointData.Properties.Last().Name);
            Assert.IsNotEmpty(touchpointData.Relations);
            Assert.AreEqual("Participants", touchpointData.Relations.First().Name);
        }
    }
}
