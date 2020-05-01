using System;
using DoMeta.Domain.CodeGen.Services;
using HandlebarsDotNet;

namespace DoMeta.Infrastructure.CodeGen
{
    public class HandlebarsTemplateEngine : ITemplateEngine
    {
        public Func<object, string> Compile(string template)
        {
            return Handlebars.Compile(template);
        }
    }
}
