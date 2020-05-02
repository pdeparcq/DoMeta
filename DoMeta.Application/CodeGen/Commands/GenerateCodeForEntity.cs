using System;
using Kledex.Commands;

namespace DoMeta.Application.CodeGen.Commands
{
    public class GenerateCodeForEntity : Command
    {
        public Guid BoundedContextId { get; set; }
        public Guid EntityId { get; set; }
        public Guid CodeTemplateId { get; set; }
    }
}
