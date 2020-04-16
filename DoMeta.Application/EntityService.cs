using System;
using System.Threading.Tasks;
using DoMeta.Application.Commands;
using DoMeta.Domain;
using DoMeta.Domain.ValueObjects;
using EnsureThat;
using Kledex;

namespace DoMeta.Application
{
    public class EntityService
    {
        private readonly IDispatcher _dispatcher;

        public EntityService(IDispatcher dispatcher)
        {
            Ensure.That(dispatcher).IsNotNull();

            _dispatcher = dispatcher;
        }

        public async Task<Entity> Register(Guid boundedContextId, string name, Property identity = null)
        {
            return await _dispatcher.SendAsync<Entity>(new RegisterEntity
            {
                BoundedContextId = boundedContextId,
                Name = name,
                Identity = identity
            });
        }
    }
}
