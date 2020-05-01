using System;

namespace DoMeta.Domain.CodeGen.Services
{
    public interface ITemplateEngine
    {
        Func<object, string> Compile(string template);
    }
}
