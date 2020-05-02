using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoMeta.Api.Models.Command;
using DoMeta.Api.Models.Query;
using DoMeta.Application.CodeGen.Commands;
using DoMeta.Application.CodeGen.Queries;
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
            await _dispatcher.SendAsync(new CreateCodeTemplate()
            {
                Name = model.Name,
                SourceType = type,
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
