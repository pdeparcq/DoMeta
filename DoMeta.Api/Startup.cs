using DoMeta.Application.Meta.Commands;
using DoMeta.Domain.CodeGen.Services;
using DoMeta.Infrastructure.CodeGen;
using DoMeta.Infrastructure.Meta;
using Kledex.Extensions;
using Kledex.Store.EF.Extensions;
using Kledex.Store.EF.SqlServer.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DoMeta.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DoMeta API", Version = "v1" });
            });

            // Configure db context for queries
            services.AddDbContext<MetaDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MetaDb")));

            services.AddDbContext<CodeGenDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CodeGenDb")));

            services.AddSingleton<CodeGenerator>();
            services.AddSingleton<ITemplateEngine>(new HandlebarsTemplateEngine());

            // Configure Kledex and db context for commands
            services.AddKledex(typeof(RegisterEntity)).AddSqlServerStore(options =>
            {
                options.ConnectionString = Configuration.GetConnectionString("DomainDb");
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddCors(options => options.AddPolicy("Cors",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MetaDbContext metaDbContext, CodeGenDbContext codeGenDbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DoMeta API V1");
            });

            // Ensure that query db is created
            metaDbContext.Database.EnsureCreated();
            codeGenDbContext.Database.EnsureCreated();

            // Use Kledex and ensure that domain db is created
            app.UseKledex().EnsureDomainDbCreated();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Cors");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
