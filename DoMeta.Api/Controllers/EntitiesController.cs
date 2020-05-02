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
    [Route("api/bc/{boundedContextId}/entities")]
    public class EntitiesController : Controller
    {
        private readonly IDispatcher _dispatcher;

        public EntitiesController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        [Route("")]
        public async Task Register([FromRoute] Guid boundedContextId, [FromBody] RegisterEntityModel model)
        {
            await _dispatcher.SendAsync(new RegisterEntity
            {
                BoundedContextId = boundedContextId,
                Name = model.Name
            });
        }

        [HttpPost]
        [Route("{id}/properties")]
        public async Task AddProperty([FromRoute] Guid id, [FromBody] AddPropertyToEntityModel model)
        {
            await _dispatcher.SendAsync(new AddPropertyToEntity()
            {
                AggregateRootId = id,
                Property = new Property(model.Name, typeof(string))
            });
        }

        [HttpPost]
        [Route("{id}/relations")]
        public async Task AddRelation([FromRoute] Guid id, [FromBody] AddRelationToEntityModel model)
        {
            await _dispatcher.SendAsync(new AddRelationToEntity()
            {
                AggregateRootId = id,
                Name = model.Name,
                MetaTypeId = model.MetaTypeId,
                Minimum = model.Minimum,
                Maximum = model.Maximum
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

            return entities.Where(e => !e.DomainEvents.Any()).Select(e => e.ToEntityModel());
        }

        
    }
}
