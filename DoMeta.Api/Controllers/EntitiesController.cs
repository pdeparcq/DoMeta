using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoMeta.Api.Models;
using DoMeta.Application.Commands;
using DoMeta.Application.Queries;
using DoMeta.Domain.ValueObjects;
using Kledex;
using Microsoft.AspNetCore.Mvc;

namespace DoMeta.Api.Controllers
{
    public class EntitiesController : Controller
    {
        private readonly IDispatcher _dispatcher;

        public EntitiesController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        [Route("")]
        public async Task Register([FromBody] RegisterEntityModel model)
        {
            await _dispatcher.SendAsync(new RegisterEntity
            {
                BoundedContextId = model.BoundedContextId,
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
        [Route("{boundedContextId}")]
        public async Task<IEnumerable<EntityModel>> GetAll([FromRoute] Guid boundedContextId)
        {
            var entities = await _dispatcher.GetResultAsync(new GetEntities
            {
                BoundedContextId = boundedContextId
            });

            return entities.Select(e => new EntityModel
            {
                BoundedContextId = e.BoundedContextId,
                Id = e.MetaTypeId,
                Name = e.Name,
                IdentityPropertyName = e.Identity.Name,
                Properties = e.Properties.Select(p => new PropertyModel
                {
                    Name = p.Name,
                    SystemType = p.SystemType,
                    MetaType = p.MetaType != null ? new MetaTypInfoModel
                    {
                        Id = p.MetaType.MetaTypeId,
                        Name = p.MetaType.Name
                    } : null
                }).ToList(),
                Relations = e.Relations.Select(r => new EntityRelationModel()
                {
                    Name = r.Name,
                    MetaType = new MetaTypInfoModel
                    {
                        Id = r.MetaType.MetaTypeId,
                        Name = r.MetaType.Name
                    },
                    Minimum = r.Minimum,
                    Maximum = r.Maximum
                }).ToList()
            });
        }
    }
}
