using System.Linq;
using DoMeta.Infrastructure.CodeGen.Entities;
using Kledex.Queries;

namespace DoMeta.Application.CodeGen.Queries
{
    public class GetCodeTemplatesBySourceType : IQuery<IQueryable<CodeTemplate>>
    {
        public string SourceType { get; set; }
    }
}
