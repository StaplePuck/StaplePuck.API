using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StaplePuck.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using StaplePuck.Core.Data;
using StaplePuck.Data.Repositories;
using GraphiQl;
using GraphQL.EntityFramework;
using GraphQL;
using GraphQL.Types;

namespace StaplePuck.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["ConnectionStrings:Default"];
            services.AddDbContext<StaplePuck.Data.StaplePuckContext>(options => options.UseNpgsql(connectionString));

            var optionsBuilder = new DbContextOptionsBuilder<StaplePuckContext>();
            optionsBuilder.UseNpgsql(connectionString);

            EfGraphQLConventions.RegisterConnectionTypesInContainer(services);
            using (var myDataContext = new StaplePuckContext(optionsBuilder.Options))
            {
                EfGraphQLConventions.RegisterInContainer(services, myDataContext);
            }
            
            
            foreach (var type in GetGraphQlTypes())
            {
                services.AddSingleton(type);
            }

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDependencyResolver>(
                provider => new FuncDependencyResolver(provider.GetRequiredService));
            services.AddSingleton<ISchema, Models.Schema>();

            var mvc = services.AddMvc();
            mvc.SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        static IEnumerable<Type> GetGraphQlTypes()
        {
            return typeof(Startup).Assembly
                .GetTypes()
                .Where(x => !x.IsAbstract &&
                            (typeof(IObjectGraphType).IsAssignableFrom(x) ||
                             typeof(IInputObjectGraphType).IsAssignableFrom(x)));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, StaplePuckContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            db.EnsureSeedData();
            app.UseGraphiQl("/graphiql", "/graphql");
            app.UseMvc();
        }
    }
}
