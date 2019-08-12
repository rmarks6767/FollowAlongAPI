//TODO 3. Compatibility Version requires Microsoft.AspNetCore.Mvc, add that here.  Also these four should be the only thing here for right now.
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
//TODO 5. Add the GraphiQL reference
using GraphiQl;

namespace FollowAlongLearnAPI
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //TODO 1. Register an implementation of the HttpServiceCollection
            services.AddHttpContextAccessor();

            //TODO 2. Add MVC and Set Compatibility Version to latest (the ASP CORE version)
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
            //TODO 4. Tell the application the GraphiQL interface, this requires the nuget package GraphiQL, you can add that now
            app.UseGraphiQl("/graph", "/graphql"); 
            //TODO 6. Add MVC to the application
            app.UseMvc();
        }
    }
}
