using System;
using System.Linq;
using System.Threading.Tasks;
using DoMeta.Application.Meta.Commands;
using DoMeta.Application.Meta.Queries;
using DoMeta.Domain.Meta;
using DoMeta.Domain.Meta.ValueObjects;
using NUnit.Framework;

namespace DoMeta.Test.Application.Meta
{
    [TestFixture]
    public class ValueObjectServiceTests : IntegrationTestBase
    {
        [Test]
        public async Task CanRegisterValueObject()
        {
            // Commands
            var address = await Dispatcher.SendAsync<ValueObject>(new RegisterValueObject()
            {
                BoundedContextId = Guid.NewGuid(),
                Name = "Address"
            });

            address = await Dispatcher.SendAsync<ValueObject>(new AddPropertyToValueObject()
            {
                AggregateRootId = address.Id,
                Property = new Property("Street", typeof(string))
            });

            // Query
            var addressData = (await Dispatcher.GetResultAsync(new GetValueObjects()
            {
                BoundedContextId = address.BoundedContextId
            })).First();

            // Verify
            Assert.IsNotNull(addressData);
            Assert.AreEqual(address.BoundedContextId, addressData.BoundedContextId);
            Assert.AreEqual(address.Id, addressData.MetaTypeId);
            Assert.AreEqual(address.Name, addressData.Name);
            Assert.IsNotEmpty(addressData.Properties);
            Assert.AreEqual("Street", addressData.Properties.First().Name);
        }
    }
}
