/*Overview of the StartUp class:
 This class is responsible for managing all of the Dependency injectable files,
 the database configuration, and all of the extra addons that you will be using 
 in your api (CORS, authentication, logging, etc).  Any thing that will be 
 dependency injected needs to be registered in this file, if not, it will never 
 be found.  
 */

//TODO 0.1.b Add the dependency injection namespace
using Microsoft.Extensions.DependencyInjection;
//TODO 0.2.b Add the graphql namespace
using GraphQL;
//TODO 0.3.b Add the ASPNETCORE namespaces
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
//TODO 0.4.b Add the GraphiQL interface namespace
using GraphiQl;

namespace FollowAlongLearnAPI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //TODO 0.1.a Register an implementation of the HttpServiceCollection
            services.AddHttpContextAccessor();

            //TODO 0.2.a Add the GraphQL nuget package and the Document Executer
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();

            //TODO 0.3.a Add MVC and Set Compatibility Version to latest (the ASP CORE version)
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //TODO 0.4.a Tell the application the GraphiQL interface, this requires the nuget package GraphiQL, you can add that now
            app.UseGraphiQl("/graph", "/graphql"); 
            //TODO 0.5 Add MVC to the application
            app.UseMvc();
        }
    }
}

//TODO 1.0 We will now make a folder called middleware.  Add two files into this folder: GraphQLController.cs and GraphQLQuery.cs
