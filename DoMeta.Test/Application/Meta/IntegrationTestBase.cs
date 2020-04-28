using System;
using DoMeta.Application.Commands;
using DoMeta.Infrastructure;
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
            ServiceProvider = serviceCollection.BuildServiceProvider();
            Dispatcher = ServiceProvider.GetService<IDispatcher>();
        }
    }
}