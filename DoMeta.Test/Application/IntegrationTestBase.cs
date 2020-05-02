using System;
using DoMeta.Application.Meta.Commands;
using DoMeta.Domain.CodeGen.Services;
using DoMeta.Infrastructure;
using DoMeta.Infrastructure.CodeGen;
using DoMeta.Infrastructure.Meta;
using Kledex;
using Kledex.Extensions;
using Kledex.Store.EF.InMemory.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DoMeta.Test.Application
{
    public class IntegrationTestBase
    {
        protected IServiceProvider ServiceProvider { get; set; }
        protected IDispatcher Dispatcher { get; set; }

        [SetUp]
        public void SetUp()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddKledex(typeof(RegisterEntity)).AddInMemoryStore();
            serviceCollection.AddDbContext<MetaDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            serviceCollection.AddDbContext<CodeGenDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            serviceCollection.AddSingleton<CodeGenerator>();
            serviceCollection.AddSingleton<ITemplateEngine>(new HandlebarsTemplateEngine());
            ServiceProvider = serviceCollection.BuildServiceProvider();
            Dispatcher = ServiceProvider.GetService<IDispatcher>();
        }
    }
}