/*Overview of the StartUp class:
 This class is responsible for managing all of the Dependency injectable files,
 the database configuration, and all of the extra add-ons that you will be using 
 in your API (CORS, authentication, logging, etc). Anything that will be dependency 
 injected needs to be registered in this file if not, it will never be found.  
 */

//TODO 1.0.1.b Add the dependency injection namespace
using Microsoft.Extensions.DependencyInjection;
//TODO 1.0.2.b Add the graphql namespace
using GraphQL;
//TODO 1.0.3.b Add the ASPNETCORE namespaces
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
//TODO 1.0.4.b Add the GraphiQL interface namespace.
using GraphiQl;
//TODO 1.2.5.b Add the references to those three classes.
using Schema = FollowAlongLearnAPI.MiddleWare.Schema;
using FollowAlongLearnAPI.MiddleWare.Queries;
using FollowAlongLearnAPI.MiddleWare.Mutations;
using GraphQL.Types;
//TODO 1.4.3.1.b Add references to those classes.
using FollowAlongLearnAPI.Model.GraphTypes;
//TODO 2.0.8.a.2 Add a reference to the Microsoft.Extensions.Configuration namespace to use the IConfiguration interface.
using Microsoft.Extensions.Configuration;
//TODO 2.0.8.d.3 Add a reference to the AccountConfiguration class we just made in the last step.
using FollowAlongLearnAPI.Configuration;
//TODO 2.3.2.b Register the repository interface and class.
using FollowAlongLearnAPI.Repositories.Interfaces;
using FollowAlongLearnAPI.Repositories;
//TODO 2.5.1.b Add the reference to those Inputs and you are done!
using FollowAlongLearnAPI.Model.Inputs;

namespace FollowAlongLearnAPI
{
    public class Startup
    {
        //TODO 2.0.8.a.1 Make a public Configuration accessor.
        public IConfiguration Configuration { get; }
        //TODO 2.0.8.b Now create a constructor for the Startup class that takes in an IConfiguration
        public Startup(IConfiguration configuration) => Configuration = configuration;  //TODO 2.0.8.c Set the configurations equal to each other.

        public void ConfigureServices(IServiceCollection services)
        {
            //TODO 1.0.1.a Register an implementation of the HttpServiceCollection.
            services.AddHttpContextAccessor();

            //TODO 1.0.2.a Add the GraphQL nuget package and the Document Executer.
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();

            //TODO 2.0.8.d.1 Now we are going to add that configuration to the services (make it dependency injectable)
            services.Configure<AccountConfiguration>(Configuration.GetSection("AccountConfiguration"));  //TODO 2.0.8.d.2 What this is doing is accessing the appsettings.json file and getting the section AccountConfiguration that we defined in there. Now that that is all registered, head on back over to the AccountRepository.

            //TODO 2.3.2.a Register the Repository as a transient, it will be a new instance of the class every time a call is made.
            services.AddTransient<IAccountRepository, AccountRepository>(); //TODO 2.3.2.c Now that it is registered, the API Should be at a point to use the GetAccount Function. Not that that is defined, we will start the Mutation end of things (Create, Delete, and Update).  Head on over to the RootMutation class to start.

            //TODO 1.2.5.a Now we will add a reference to the RootQuery, RootMutation, and the Schema.  Note that the Schema has to be last, as it will not have all the stuff we injected if it wasn't.
            services
                .AddScoped<RootQuery>()
                .AddScoped<RootMutation>()

            //TODO 1.4.3.1.a Now add the AccountQuery (If you decided to go that route), the AccountGraph, the AddressGraph, and the NameGraph.
                .AddScoped<AccountQuery>()
                .AddScoped<AccountGraph>()
                .AddScoped<AddressGraph>()
                .AddScoped<NameGraph>()
            
            //TODO 2.5.1.a Add the AccountMutation, AccountInput, AddressInput, and the NameInput to the services.
                .AddScoped<AccountMutation>()
                .AddScoped<AccountInput>()
                .AddScoped<AddressInput>()
                .AddScoped<NameInput>()

                .AddSingleton<ISchema, Schema>();

            //TODO 1.0.3.a Add MVC and Set Compatibility Version to latest (the ASP CORE version)
            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Latest);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            //TODO 1.0.4.a Tell the application the GraphiQL interface, this requires the nuget package GraphiQL, you can add that now
            app.UseGraphiQl("/graph", "/graphql");
            //TODO 1.0.5 Add MVC to the application
            app.UseMvc();
        }
    }
}
//TODO 1.1.0 We will now make a folder called middleware.  Add two files into this folder: GraphQLController.cs and GraphQLQuery.cs

//TODO 1.3.0 Now it's time to actually start making the logic behind all of the API.  For our example, we will do account creation, account querying, and account deletion.  We will start by defining the models for an account.  Start by creating a Model folder and a Base folder in that. Add a file called Account.cs to that base folder.

//TODO 1.4.4 Give it a run! (hit ctrl + F5 and head to https://localhost:5001/graph).  From there you should be able to see the docs portion, which outlines your whole schema. In the top left box enter the following:
/*
**Use this one if you decided to create the new class for the AccountQuery
 query {
  account {
    account {
      address {
        zipcode
        state
        boxNumber
        street
      }
      password
      userName
      name {
        middleInitial
        lastName
        firstName
      }
    }
  }
}

**Use this one if you decided NOT to create the new class for the AccountQuery
query {
  account {
    address {         
      zipcode
      state
      boxNumber
      street
    }
    password
    userName
    name {
      middleInitial
      lastName
      firstName
    }
  }
}
 */

//TODO 1.4.5 THIS ENDS MODULE 1. CONTINUE ON TO MODULE 2 BELOW ↓↓↓

//TODO 2.0.0 Module 2 will cover the Logic of getting the data.  To start, create a new folder called Repositories and add a class called AccountRepository.

//TODO 1.6 THAT ENDS MODULE 2.  ADVANCED TIPS AND TRICKS COMING SOON.