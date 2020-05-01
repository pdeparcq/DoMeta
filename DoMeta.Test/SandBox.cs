using System;
using HandlebarsDotNet;
using NUnit.Framework;

namespace DoMeta.Test
{
    [TestFixture]
    public class SandBox
    {
        [Test]
        public void CanUseHandleBars()
        {
            string source =
                @"<div class=""entry"">
                  <h1>{{title}}</h1>
                  <div class=""body"">
                    {{body}}
                  </div>
                </div>";

            var template = Handlebars.Compile(source);

            var data = new
            {
                title = "My new post",
                body = "This is my first post!"
            };

            var result = template(data);

            Console.WriteLine(result);
        }
    }
}
