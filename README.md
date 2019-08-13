# Creating a new Project
**0.0** Create a new empty ASP .NET CORE web application
Upon creating a new project, you will be presented with three files (Startup.cs, program.cs, launchSettings.json).  We are going to add some stuff to them now.

## Program.cs
**0.0.a** This is where the startup will be called. <br>
**0.0.b** Let's also take this time to edit the launchSettings.json file, you'll find in that file what should be in there.

## Startup.cs
 This class is responsible for managing all of the Dependency injectable files,
 the database configuration, and all of the extra add-ons that you will be using in your API (CORS, authentication, logging, etc).  Anything that will be dependency injected needs to be registered in this file if not, it will never be found.  <br>
**0.1.a** Register an implementation of the HttpServiceCollection.<br>
**0.1.b** Add the dependency injection namespace.<br>

**0.2.a** Add the GraphQL NuGet package and the Document Executer.<br>
**0.2.b** Add the GraphQL namespace.<br>

**0.3.a** Add MVC and Set Compatibility Version to the latest (the ASP CORE version).<br>
**0.3.b** Add the ASPNETCORE namespaces.<br>

**0.4.a** Tell the application the GraphiQL interface, this requires the NuGet package GraphiQL, you can add that now.<br>

**0.5** Add MVC to the application.<br>

# Middleware Base Files
**1.0** We will now make a folder called middleware.  Add two files into this folder: ```GraphQLController.cs``` and ```GraphQLQuery.cs```

## GraphQLController.cs (Pt. 1)
The controller is where all the requests will actually be made to.  The endpoint, as defined by the name {something}Controller, allows the user to request different data in a traditional REST API model.  GraphQL allows this to behave a little differently.  Instead of having multiple controllers, you will have one. That one is responsible for controlling the logic and directing all of the requests made to the API.  For our example here, our main endpoint will be https://localhost:5001/graphql. That endpoint directly corresponds to this document. To view the schema you will want to direct your attention to https://localhost:5001/graph, where the GraphiQL interface will be displayed. 

**1.1.a** We are now going to specify that the endpoint to get here is /graphql (it comes from the characters you put in front of the controller in the name of the class)<br>
**1.1.b** Add MVC namespace.<br>

**1.2.a** Make a constructor and add an injection of an ISchema (which will be the schema) and an IDocumentExecuter (for the execution of the schema).<br>
**1.2.b** Add GraphQL for the Executer and GraphQL.Types for the ISchema.<br>
**1.2.c** Now add private read-only fields for the executor and the schema.<br>
**1.2.d** Assing the dependency injected fields to the private read-only ones.<br>

**1.3.a** We will now add the main POST entry point for the API.<br>
**1.3.b** Add threading to the namespace.<br>
**1.3.c** Now head over (or create) the GraphQLQuery.cs file.  That will be how these requests are translated so it has to be done before we move forward with the controller.<br>

## GraphQLQuery.cs
**1.4.a** Operation is in relation to Query or Mutation when it comes to GraphQL.<br>
**1.4.b** NamedOperation is in relation to the specific function in query or mutation that you are going to perform.<br>
**1.4.c** The query is the information that you request from the database.<br>
**1.4.d** The inputs are any variables that you may have in your query.<br>
**1.4.d.1** Add / install the Newtonsoft Json NuGet package.<br>
**1.4.e** Now that that's finished, head back over to the controller.<br>

## GraphQLController.cs (Pt. 2)
**1.5.a** After defining the GraphQLQuery, we can use it by saying [FromBody], which will convert the JSON post body to the object we just created.<br>
**1.5.b** Now we are going to make sure the query is not null if it is returned BadRequest.<br>
**1.5.c** Now we will build the Execution Options to pass to the document executer.<br>
**1.5.c.1** Set the schema equal to the one we injected.<br>
**1.5.c.2** Set the query equal to the query from the custom object.<br>
**1.5.c.3** Set any variables equal to the inputs.<br>
**1.5.d** We will now send that to the executor, which will resolve the query and return a result.<br>
**1.5.e.1** With that result, we will check for errors and report them as necessary.<br>
**1.5.e.2** For now we will just return the errors, but coming up with how you want to report the errors is important. I would suggest reporting errors, data, and a status code that corresponds to any errors that occurred.<br>
**1.5.f** Now that we have checked if there are any errors, we can return the normal data.<br>

**1.6** For now the controller is done.  Now it's time to build a schema and add some logic into the mix. Make a file called Schema.cs and put it into the Middleware folder.<br>

## Schema.cs 
The schema is what actually makes up GraphQL.  Defining logic in 'Graphs' allows us to connect data without making it all relational. This is the root upon which all 'endpoints' will be defined.  In a traditional model, this corresponds to different endpoints and controllers you would have to make.  By using GraphQL, you alleviate the need to make a new controller every time you have new data available.  This makes versioning seemingly effortless and allows you to move forward with your API effortlessly.

**2.0.a** First, add inheritance to GraphQL.Types.Schema, we can do it by adding a reference to the schema under a different name using GQLSchema = GraphQL.Types.Schema to the namespace.<br>
**2.0.b** rename the reference to the GraphQL Schema type (because it has the same name as our class)<br>

**2.1.a** Now make a constructor for our new Schema type.<br>
**2.1.b** To resolve the types, GraphQL uses IServiceProvider (This comes from all the types that are injected in the Startup class).<br>
**2.1.c** Add a reference to System to use the IServiceProvider.<br>
**2.1.d** This is where we will add a reference to the RootQuery and RootMutation.  Make those files now in the middleware folder.  These should be in their own folders called Queries and Mutations respectively.<br>
**2.1.e.1** Now we can add a reference to both of those classes.  You should get errors because we have not set up either of those classes yet.  <br>
**2.1.e.2** Add These namespaces to use the query, mutation, and the GetRequiredService Extension. <br>
**2.2** The Schema is now all done so let's head over to the RootQuery class and set that one up.<br>

# Middleware Meat and Potatoes
## RootQuery.cs















