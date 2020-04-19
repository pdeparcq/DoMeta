using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoMeta.Api.Models;
using DoMeta.Application.Commands;
using DoMeta.Application.Queries;
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
