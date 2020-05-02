using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoMeta.Api.Models.Command;
using DoMeta.Api.Models.Query;
using DoMeta.Application.CodeGen.Commands;
using DoMeta.Application.CodeGen.Queries;
using DoMeta.Domain.CodeGen;
using Kledex;
using Microsoft.AspNetCore.Mvc;

namespace DoMeta.Api.Controllers
{

    [Route("api/templates/{type}")]
    public class CodeTemplatesController : Controller
    {
        private readonly IDispatcher _dispatcher;

        public CodeTemplatesController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        [Route("")]
        public async Task Create([FromRoute] string type, [FromBody] CreateCodeTemplateModel model)
        {
            var template = await _dispatcher.SendAsync<CodeTemplate>(new CreateCodeTemplate()
            {
                Name = model.Name,
                SourceType = type,
            });

            if (model.Value != null)
            {
                await _dispatcher.SendAsync(new UpdateCodeTemplate()
                {
                    AggregateRootId = template.Id,
                    Value = model.Value
                });
            }
        }

        [HttpPut]
        [Route("{templateId}")]
        public async Task Update([FromRoute] string type, [FromRoute] Guid templateId, [FromBody] UpdateCodeTemplateModel model)
        {
            await _dispatcher.SendAsync(new UpdateCodeTemplate()
            {
                AggregateRootId = templateId,
                Value = model.Value
            });
        }

        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<CodeTemplateModel>> GetAll([FromRoute] string type)
        {
            var templates = await _dispatcher.GetResultAsync(new GetCodeTemplatesBySourceType()
            {
                SourceType = type
            });

            return templates.Select(t => new CodeTemplateModel
            {
                Id = t.Id,
                Name = t.Name,
                Value = t.Value
            });
        }
    }
}
