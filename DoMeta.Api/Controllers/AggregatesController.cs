using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoMeta.Api.Models;
using DoMeta.Api.Models.Command;
using DoMeta.Api.Models.Query;
using DoMeta.Application.Meta.Commands;
using DoMeta.Application.Meta.Queries;
using DoMeta.Domain.Meta.ValueObjects;
using Kledex;
using Microsoft.AspNetCore.Mvc;

namespace DoMeta.Api.Controllers
{
    [Route("api/bc/{boundedContextId}/aggregates")]
    public class AggregatesController : Controller
    {
        private readonly IDispatcher _dispatcher;

        public AggregatesController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        [Route("")]
        public async Task Register([FromRoute] Guid boundedContextId, [FromBody] RegisterAggregateModel model)
        {
            await _dispatcher.SendAsync(new RegisterEntity
            {
                BoundedContextId = boundedContextId,
                Name = model.Name,
                AggregateDomainEventName = model.DomainEventName
            });
        }

        [HttpPost]
        [Route("{id}/events")]
        public async Task AddDomainEvent([FromRoute] Guid id, [FromBody] AddDomainEventToAggregateModel model)
        {
            await _dispatcher.SendAsync(new AddDomainEventToAggregate()
            {
                AggregateRootId = id,
                Name = model.Name
            });
        }

        [HttpPost]
        [Route("{id}/events/{name}/properties")]
        public async Task AddPropertyToDomainEvent([FromRoute] Guid id, [FromRoute] string name, [FromBody] AddPropertyToDomainEventModel model)
        {
            await _dispatcher.SendAsync(new AddPropertyToDomainEvent()
            {
                AggregateRootId = id,
                DomainEventName = name,
                Property = new Property(model.Name, typeof(string))
            });
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<EntityModel>> GetAll([FromRoute] Guid boundedContextId)
        {
            var entities = await _dispatcher.GetResultAsync(new GetEntities
            {
                BoundedContextId = boundedContextId
            });

            return entities.Where(e => e.DomainEvents.Any()).Select(e => e.ToEntityModel());
        }
    }
}
