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
                Name = e.Name
            });
        }
    }
}
