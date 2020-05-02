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
    [Route("api/bc/{boundedContextId}/valueObjects")]
    public class ValueObjectsController : Controller
    {
        private readonly IDispatcher _dispatcher;

        public ValueObjectsController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        [Route("")]
        public async Task Register([FromRoute] Guid boundedContextId, [FromBody] RegisterValueObjectModel model)
        {
            await _dispatcher.SendAsync(new RegisterValueObject()
            {
                BoundedContextId = boundedContextId,
                Name = model.Name
            });
        }

        [HttpPost]
        [Route("{id}/properties")]
        public async Task AddProperty([FromRoute] Guid id, [FromBody] AddPropertyToValueObjectModel model)
        {
            await _dispatcher.SendAsync(new AddPropertyToValueObject()
            {
                AggregateRootId = id,
                Property = new Property(model.Name, typeof(string))
            });
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<ValueObjectModel>> GetAll([FromRoute] Guid boundedContextId)
        {
            var valueObjects = await _dispatcher.GetResultAsync(new GetValueObjects()
            {
                BoundedContextId = boundedContextId
            });

            return valueObjects.Select(v => v.ToValueObjectModel());
        }
    }
}
