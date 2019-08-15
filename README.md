*The best way to learn something new is to dive head first into a project with little to no knowledge.  This mini tutorial encompasses that idea and guides you along the way.  Below are all the comments you can find scattered throughout the project, leading you to understand where every piece of code came from, as well as why you put it there.  If there's anything that needs a little bit more explaining, feel free to open a PR and let me know!  I hope you enjoy this unique take on a tutorial!*
<a name="top"></a>


# Table of Contents
* [Module 1: Before the Logic](#module1)
  * [Creating a new Project](#creating)
    * [Program.cs](#program)
    * [Startup.cs](#startup)
  * [Middleware Base Files](#middleware1)
    * [GraphQLController.cs (Pt. 1)](#controller1)
    * [GraphQLQuery.cs](#query)
    * [GraphQLController.cs (Pt. 2)](#controller2)
    * [Schema.cs](#schema)
  * [Middleware Meat and Potatoes](#middleware2)
    * [RootQuery.cs](#rootquery1)
    * [RootMutation.cs](#rootmutation1)
    * [Registering Schema, RootMutation, and RootQuery](#registration)
  * [Account Model](#accountmodel)
    * [Account.cs](#account)
      * [Name.cs](#name)
      * [Address.cs](#address)
    * [AccountGraph.cs](#accountgraph)
      * [NameGraph.cs](#namegraph)
      * [AddressGraph.cs](#addressgraph)
  * [Query Functionality](#functionality)
    * [RootQuery](#rootquery2)
    * [(Optional) AccountQuery.cs](#accountquery)
    * [Startup (Pt. 3)](#startup3)
    * [Giving it a Try](#giveitatry)
* [Module 2: Defining the Logic](#logicdefinition)
  * [AccountRepository.cs Setup](#accountreposetup)
    * [IAccountRepository.cs Creation](#Iaccountcreation)
  * [Database Configuration](#dbconfig)
    * [AccountConfiguration.cs](#accountconfig)
    * [Appsettings.json](#appsettings)
    * [Startup.cs (Pt. 4)](#startup4)
  * [AccountRepository.cs Logic](#accountlogic)
    * [The Functions](#functions)
      * [GetAccount](#getaccount)
      * [CreateAccount](#createaccount)
      * [UpdateAccount](#updateaccount)
      * [DeleteAccount](#deleteaccount)
  * [IAccountRepository.cs Definitions](#Iaccountrepo)
  * [The Query logic](#querylogic)
    * [(Option Dependent) RootQuery Logic](#rootquerylogic)
    * [(Option Dependent) AccountQuery Logic](#accountquerylogic)
    * [Startup (Pt. 5)](#startup5)
  * [RootMutation Logic](#rootmutationlogic)
    * [AccountMutation](#accountmutation)
      * [Field Logic](#fieldlogic)
        * [Create Function](#create)
        * [Delete Function](#delete)
        * [Update Function](#update)
  * [Startup... Is it Finally Over?](#startupdone)
  * [It is Finally over.](#over)
* [Module 3: Advanced Tips and Tricks](#advanced)
  * [Scalar Types](#scalars)
  * [Error Handling](#errorhandling)
  * [Making Custom Types](#customtypes)

# Module 1: Before the Logic <a name="module1"></a>
In this module we will cover the basics of using .NetCore with GraphQL.  We will make a simple Account object and by the end be able to query our account object.  We will also cover the basics of a C# Web API and the things you'll need from that to get GraphQL DotNet to operate properly.  You can find the source of this module at this [commit](https://github.com/rmarks6767/FollowAlongAPI/commit/46597cda7fa4a63ecfc50e1fe871aaeb984e67e6) or by downloading the source from the [release](https://github.com/rmarks6767/FollowAlongAPI/releases/tag/v1.0)

## Creating a new Project <a name="creating"></a>
**1.0.0** Create a new empty ASP .NET CORE web application
Upon creating a new project, you will be presented with three files (Startup.cs, program.cs, launchSettings.json).  We are going to add some stuff to them now.

### Program.cs <a name="program"></a>
**1.0.0.a** This is where the startup will be called. <br>
**1.0.0.b** Let's also take this time to edit the launchSettings.json file, you'll find in that file what should be in there.<br>
**1.0.0.c** Remove all but this from the launch settings file.

### Startup.cs <a name="startup"></a>
 This class is responsible for managing all of the Dependency injectable files,
 the database configuration, and all of the extra add-ons that you will be using in your API (CORS, authentication, logging, etc).  Anything that will be dependency injected needs to be registered in this file if not, it will never be found.  <br>
**1.0.1.a** Register an implementation of the HttpServiceCollection.<br>
**1.0.1.b** Add the dependency injection namespace.<br>

**1.0.2.a** Add the GraphQL NuGet package and the Document Executer.<br>
**1.0.2.b** Add the GraphQL namespace.<br>

**1.0.3.a** Add MVC and Set Compatibility Version to the latest (the ASP CORE version).<br>
**1.0.3.b** Add the ASPNETCORE namespaces.<br>

**1.0.4.a** Tell the application the GraphiQL interface, this requires the NuGet package GraphiQL, you can add that now.<br>

**1.0.5** Add MVC to the application.<br>

## Middleware Base Files <a name="middleware1"></a>
**1.1.0** We will now make a folder called middleware.  Add two files into this folder: ```GraphQLController.cs``` and ```GraphQLQuery.cs```

### GraphQLController.cs (Pt. 1) <a name="controller1"></a>
The controller is where all the requests will actually be made to.  The endpoint, as defined by the name {something}Controller, allows the user to request different data in a traditional REST API model.  GraphQL allows this to behave a little differently.  Instead of having multiple controllers, you will have one. That one is responsible for controlling the logic and directing all of the requests made to the API.  For our example here, our main endpoint will be https://localhost:5001/graphql. That endpoint directly corresponds to this document. To view the schema you will want to direct your attention to https://localhost:5001/graph, where the GraphiQL interface will be displayed. 

**1.1.1.a** We are now going to specify that the endpoint to get here is /graphql (it comes from the characters you put in front of the controller in the name of the class)<br>
**1.1.1.b** Add MVC namespace.<br>

**1.1.2.a** Make a constructor and add an injection of an ISchema (which will be the schema) and an IDocumentExecuter (for the execution of the schema).<br>
**1.1.2.b** Add GraphQL for the Executer and GraphQL.Types for the ISchema.<br>
**1.1.2.c** Now add private read-only fields for the executor and the schema.<br>
**1.1.2.d** Assing the dependency injected fields to the private read-only ones.<br>

**1.1.3.a** We will now add the main POST entry point for the API.<br>
**1.1.3.b** Add threading to the namespace.<br>
**1.1.3.c** Now head over (or create) the GraphQLQuery.cs file.  That will be how these requests are translated so it has to be done before we move forward with the controller.<br>

### GraphQLQuery.cs <a name="query"></a>
**1.1.4.a** Operation is in relation to Query or Mutation when it comes to GraphQL.<br>
**1.1.4.b** NamedOperation is in relation to the specific function in query or mutation that you are going to perform.<br>
**1.1.4.c** The query is the information that you request from the database.<br>
**1.1.4.d** The inputs are any variables that you may have in your query.<br>
**1.1.4.d.1** Add / install the Newtonsoft Json NuGet package.<br>
**1.1.4.e** Now that that's finished, head back over to the controller.<br>

### GraphQLController.cs (Pt. 2) <a name="controller2"></a>
**1.1.5.a** After defining the GraphQLQuery, we can use it by saying [FromBody], which will convert the JSON post body to the object we just created.<br>
**1.1.5.b** Now we are going to make sure the query is not null if it is returned BadRequest.<br>
**1.1.5.c** Now we will build the Execution Options to pass to the document executer.<br>
**1.1.5.c.1** Set the schema equal to the one we injected.<br>
**1.1.5.c.2** Set the query equal to the query from the custom object.<br>
**1.1.5.c.3** Set any variables equal to the inputs.<br>
**1.1.5.d** We will now send that to the executor, which will resolve the query and return a result.<br>
**1.1.5.e.1** With that result, we will check for errors and report them as necessary.<br>
**1.1.5.e.2** For now we will just return the errors, but coming up with how you want to report the errors is important. I would suggest reporting errors, data, and a status code that corresponds to any errors that occurred.<br>
**1.1.5.f** Now that we have checked if there are any errors, we can return the normal data.<br>

**1.1.6** For now the controller is done.  Now it's time to build a schema and add some logic into the mix. Make a file called Schema.cs and put it into the Middleware folder.<br>

### Schema.cs <a name="schema"></a>
The schema is what actually makes up GraphQL.  Defining logic in 'Graphs' allows us to connect data without making it all relational. This is the root upon which all 'endpoints' will be defined.  In a traditional model, this corresponds to different endpoints and controllers you would have to make.  By using GraphQL, you alleviate the need to make a new controller every time you have new data available.  This makes versioning seemingly effortless and allows you to move forward with your API effortlessly.

**1.2.0.a** First, add inheritance to GraphQL.Types.Schema, we can do it by adding a reference to the schema under a different name using GQLSchema = GraphQL.Types.Schema to the namespace.<br>
**1.2.0.b** rename the reference to the GraphQL Schema type (because it has the same name as our class)<br>

**1.2.1.a** Now make a constructor for our new Schema type.<br>
**1.2.1.b** To resolve the types, GraphQL uses IServiceProvider (This comes from all the types that are injected in the Startup class).<br>
**1.2.1.c** Add a reference to System to use the IServiceProvider.<br>
**1.2.1.d** This is where we will add a reference to the RootQuery and RootMutation.  Make those files now in the middleware folder.  These should be in their own folders called Queries and Mutations respectively.<br>
**1.2.1.e.1** Now we can add a reference to both of those classes.  You should get errors because we have not set up either of those classes yet.  <br>
**1.2.1.e.2** Add These namespaces to use the query, mutation, and the GetRequiredService Extension. <br>
**1.2.2** The Schema is now all done so let's head over to the RootQuery class and set that one up.<br>

## Middleware Meat and Potatoes <a name="middleware2"></a>

### RootQuery.cs <a name="rootquery1"></a>
**1.2.0.a** Add inheritance to ObjectGraphType, a GraphQL type that allows it to know it belongs to GraphQL <br>
**1.2.0.b** Add a reference to GraphQL.Types to use the ObjectGraphType<br>

**1.2.1.a** Create the constructor for the RootQuery<br>
**1.2.1.b** Now give it a name.  It's always smart to name your queries to the base type that they are defining. IE: we will name this one 'Query'<br>
**1.2.1.c** Also getting in the habit of giving each Graph Type a description will help the front end a lot.<br>
**1.2.1.d** After we create query classes, they will be defined here: We'll come back here after a little while. Now head over to the RootMutation.<br>

### RootMutation.cs <a name="rootmutation1"></a>
**1.2.3.a** Add inheritance to ObjectGraphType, a GraphQL type that allows it to know it belongs to GraphQL <br>
**1.2.3.b** Add a reference to GraphQL.Types to use the ObjectGraphType<br>

**1.2.4.a** Create the constructor for the RootQuery<br>
**1.2.4.b** Now give it a name, for the same reason you gave the mutation a name<br>
**1.2.4.c** Also getting in the habit of giving each Graph Type a description will help the front end a lot.<br>
**1.2.4.d** The field definitions will go here.  First, let's go back to the Startup class and add the files we just created.<br>

### Registering Schema, RootMutation, and RootQuery <a name="registration"></a>
**1.2.5.a** Now we will add a reference to the RootQuery, RootMutation, and the Schema.  Note that the Schema has to be last, as it will not have all the stuff we injected if it wasn't.<br>
**1.2.5.b** Add the references to those three classes.<br>

## Account Model <a name="accountmodel"></a>
**1.3.0** Now it's time to actually start making the logic behind all of the API.  For our example, we will do account creation, account querying, and account deletion.  We will start by defining the models for an account.  Start by creating a Model folder and a Base folder in that. Add a file called Account.cs to that base folder.<br>

### Account.cs <a name="account"></a>
**1.3.1** Now we are going to add some basic stuff to the Account object, name, address, etc.<br>
**1.3.2.a** We are going to make a separate Name type that will be stored on the Account.  This will allow us to use it anywhere.  Make that file in the model folder.<br>

#### Name.cs <a name="name"></a>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**1.3.2.b** Add a first name, last name, and middle initial fields here.<br>

**1.3.3.a** For the Address, we are going to make another type, add that to the model folder now.<br>

#### Address.cs <a name="address"></a>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**1.3.3.b** Add some fields here as well<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**1.3.3.c** That should be enough for now.  Head back over to the Account class and we will continue from there.<br>

**1.3.4** Now that the Account model is defined, we will make Graph types for these three models.  Make a folder called GraphTypes in the Model folder.  Add a type called AccountGraph to this folder.<br>

### AccountGraph.cs <a name="accountgraph"></a>
**1.3.5.a** Add inheritance to the ObjectGraphType (translating to GraphQL) and have that have the type of Account.<br>
**1.3.5.b** Add the references to the Account class and the ObjectGraphType.<br>

**1.3.6.a** Now create the constructor for the AccountGraph.<br>
**1.3.6.b** Add a name and description to the AccountGraph.<br>

**1.3.7.a** Now we are going to define each of the fields on the account type as a GraphQL type.<br>
**1.3.7.a.1** Right here we are saying "GraphQL this field should be exposed for querying (username) and its name should be userName".<br>
**1.3.7.b** Now do that for the rest of the fields. NOTE this can only be done for types that are Primitive, if there's a type that is different, like Name, you will have to make a separate Graph Type for it.<br>

**1.3.8.a** Now create a GraphType for the Name model type.  Put it in the same GraphTypes folder.<br>

#### NameGraph.cs <a name="namegraph"></a>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**1.3.8.b.1** This one will be set up very similarly to the NameGraph, add inheritance and map all of the fields.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**1.3.8.b.2** Add references to the classes used with the inheritance.<br>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**1.3.8.c.1** Make a constructor for the NameGraph type.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**1.3.8.c.2** Add a name and description to the AddressGraph.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**1.3.8.c.3** Define all of the fields from the Name class.<br>

**1.3.8.d** Now that we created that GraphType, we are going to register it.<br>

**1.3.9.a** Now create a GraphType for the Address model type.  Put it in the same GraphTypes folder.<br>

#### AddressGraph.cs <a name="addressgraph"></a>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**1.3.8.b.1** This one will be set up very similarly to the AccountGraph, add inheritance and map all of the fields.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**1.3.8.b.2** Add references to the classes used with the inheritance.<br>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**1.3.8.c.1** Make a constructor for the AddressGraph type.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**1.3.8.c.2** Add a name and description to the AddressGraph.<br>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**1.3.8.c.3** Define all of the fields from the Address class.<br>

**1.3.9.d** Now that we created that GraphType, we are going to register it.<br>

**1.3.9.x / 1.3.8.x** Here we are saying that this type has to be broken down more so go to that file to resolve the type that we pass it.<br>

## Query Functionality <a name="functionality"></a>
**1.4.0** Now we are ready to define the functionality of this new type.  We'll start with the Query portion of this Type, then we will move into the mutation section.  Head on over to the RootQuery class. <br>

### RootQuery <a name="rootquery2"></a>
**1.4.1.a** The first method we can use to defining the queries is to have all of the functionality be here.  Alternatively, we can create a separate file, to see this method follow along with the 4.2 path.<br>
**1.4.1.b** For this given instance, it is okay to keep all of this functionality in one file because all we have is the AccountGraph.  If your file were to be bigger than just one type, I would advise using the second method. Now we are going to create a field.  This is going to be how we get the actual value of the account, it will come through here.<br>
**1.4.1.c.1** We are going to set this to AccountGraph, this is the return type of the field.<br>
**1.4.1.c.2** Add a reference to the AccountGraph and the Account, Address, and Name types.<br>
**1.4.1.d** For now, we are just going to return a new Account with some default values.  This will be like this until we write the repository logic.<br>

### (Optional) AccountQuery.cs <a name="accountquery"></a>

**1.4.2.a** Second method, creating another file called AccountQuery.cs.  To tell the RootQuery that there is another Query you create an empty version of the file and set that as the resolver.  This is also the first time that you are seeing the resolver.  The resolver is responsible for determining what file to inject using the IServiceProvider from the Schema.cs file.  It will make more sense as time goes on.<br>

**1.4.2.b** When we say new { }, we are essentially adding all of the functionality of AccountQuery to the RootQuery.cs file.  Giving it a field makes it so there is another level of declaration before getting to the base functionality.<br>

**1.4.2.c** Now add the AccountQuery file to the queries folder.<br>

**1.4.2.d.1** Add inheritance to ObjectGraphType to tell GraphQL that it is a GraphQL type.<br>
**1.4.2.d.2** Add a reference to the ObjectGraphType.<br>

**1.4.2.e.1** Add the Constructor for the AccountQuery.<br>
**1.4.2.e.2** Give the Query a Name and Description.<br>
**1.4.2.e.3** Now we are going to create a field.  This is going to be how we get the actual value of the account, it will come through here.<br>
**1.4.2.e.3.a** We are going to set this to AccountGraph, this is the return type of the field.<br>
**1.4.2.e.3.b** Add a reference to the AccountGraph type.<br>
**1.4.2.e.4.a** For now, we are just going to return a new Account with some default values.  This will be like this until we write the repository logic.<br>
**1.4.2.e.4.b** Add a reference to the Account class.<br>

### Startup (Pt. 3) <a name="startup3"></a>
**1.4.3.1.a** Now add the AccountQuery (If you decided to go that route), the AccountGraph, the AddressGraph, and the NameGraph.
**1.4.3.1.b** Add references to those classes.<br>

### Giving it a Try <a name="giveitatry"></a>
**1.4.4** Give it a run! (hit ctrl + F5 and head to https://localhost:5001/graph).  From there you should be able to see the docs portion, which outlines your whole schema. In the top left box enter the following:<br>

Use this one if you decided to create the new class for the AccountQuery
```gql
 query {
  accountQuery {
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
```
Use this one if you decided NOT to create the new class for the AccountQuery
```gql
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
```
This concludes Module 1.  Continue on to Module 2 Below or back to the [top](#top)

# Module 2: Defining the Logic <a name="logicdefinition"></a>
Module 2 will cover the Logic of getting the data.  We will also cover making, deleting, and updating accounts.  To start, create a new folder called Repositories and add a class called AccountRepository.
## AccountRepository.cs Setup <a name="accountreposetup"></a>
**2.0.1.a** Now we will create an Interface for the repository so that it can be injected by the interface. Create this file in the Repositories folder in another folder called Interfaces.  Call this file IAccountRepository.

### IAccountRepository.cs Creation <a name="Iaccountcreation"></a>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;**2.0.1.b** We will fill this once we create the methods in the AccountRepository class.

**2.0.2.a** Now add inheritance to the interface that you just created.
**2.0.2.b** Add a reference to the Interfaces folder.

**2.0.3** Before we move forward with the AccountRepository we need to make a configuration of the database info.  Create a file called AccountConfiguration.  You can put this in a folder called Configuration.

## Database Configuration <a name="dbconfig"></a>
This ultimately will allow you to connect to a Cosmos DB and insert/retrieve data from there.

### AccountConfiguration.cs <a name="accountconfig"></a>
**2.0.3.a** The example database that we are going to use is Cosmos DB.  There is an emulator that you can download that will work perfectly for what we are doing. --> [Download Here](https://aka.ms/cosmosdb-emulator)
**2.0.3.b** The URI of the CosmosDb
**2.0.3.c** The Primary Key for access to the Database.
**2.0.3.d** The Name of the Database that the accounts are stored in. 
**2.0.3.e** The collection that the accounts are stored in.

**2.0.4.a** Now we are going to make a function that will return the Document client whenever we need it.
**2.0.4.b** You will need to add the Microsoft.Azure.Documents NuGet Package here if you have not already, then add a reference to it.
**2.0.4.c** Cosmos relies on a connection policy, URI, and a Primary key for access.  We are going to make a new connection policy now.
**2.0.4.d.1** Now that we have set all of the connection policy fields, we can return a new DocumentClient.
**2.0.4.d.2** Add a reference to System to use the Uri extension.

**2.0.5** Now make some helper functions to return different URIs for different documents. (Basically, make it easier to connect to Cosmos)

**2.0.6** Now we are going to set up the JSON file to properly populate the above fields with data.  At this time head over to the appsettings.json file.

### Appsettings.json <a name="appsettings"></a>
**2.0.6.a** Add a section named AccountConfiguration.  Add the Uri (This is the one that the emulator resides at), the key (also it should be the same key the emulator uses the same one), and set the DB name and the collections name to whatever you like.  I chose Account and Accounts because that makes the most sense.
An example of what should be in the file is as follows:
```json
{
  "AccountConfiguration": {
    "Uri": "https://localhost:8081",
    "Key": "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
    "AccountDB": "Account",
    "AccountCollection": "Accounts"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```
**2.0.7** Now that that's done, we have to add it to the startup.  Head there now.

### Startup.cs (Pt. 4) <a name="startup4"></a>
**2.0.8.a.1** Make a public Configuration accessor.
**2.0.8.a.2** Add a reference to the Microsoft.Extensions.Configuration namespace to use the IConfiguration interface.
**2.0.8.b** Now create a constructor for the Startup class that takes in an IConfiguration.
**2.0.8.c** Set the configurations equal to each other.
**2.0.8.d.1** Now we are going to add that configuration to the services (make it dependency injectable).
**2.0.8.d.2** What this is doing is accessing the appsettings.json file and getting the section AccountConfiguration that we defined in there. Now that that is all registered, head on back over to the AccountRepository.
**2.0.8.d.3** Add a reference to the AccountConfiguration class we just made in the last step.

## AccountRepository.cs Logic <a name="accountlogic"></a>
**2.1.0.a.1** Now that the Configuration is all set up, we are ready to move full steam ahead.  Here, we are going to add a document client field that the file can use to establish a connection with Cosmos.
**2.1.0.a.2** This will give us all of the information that we need to connect with Cosmos.  We are going to set it in the constructor.
**2.1.0.b** Add a reference to the DocumentClient.

**2.1.0.c** Now, we are going to set the client in the constructor of the AccountRepository.
**2.1.0.c.1** This will take in a version of the Configuration that we set up. 
**2.1.1.c.2** Add a reference to IQueryable to use in the function.
**2.1.0.c.3** We then set the dbhandler equal to the configuration file we set up and get the Document Client from the handler.

### The Functions <a name="functions"></a>
**2.1.1.a** Now we will create four functions, create, delete, update, and get.  We will start with the get because that one will be the most straight forward.

#### GetAccount <a name="getaccount"></a>
**2.1.1.b** Add a reference to the Account type.
**2.1.1.c** Put the entire thing in a try-catch because it could fail if Cosmos is not online.
**2.1.1.d** We will now query Cosmos using the CreateDocumentQuery Function.  We specify that it is of type Account so that it will return an object that is an account.
**2.1.1.e** We then say that the userName (which will be our partition key) must equal the username that we provide. 
**2.1.1.f** We then return the first result that we get from Cosmos.
**2.1.1.g** If it fails for whatever reason, we will return a default of the Account type.

#### CreateAccount <a name="createaccount"></a>
**2.1.2.a.1** Now we will make the Account Creation function
**2.1.2.a.2** Add references to the Document and Task for use in the create function.  Also, a status code to use to see if there was a success or failure.
**2.1.2.b** We will also encapsulate this in a try-catch, in case of any errors.
**2.1.2.c** We will create the new account, if anything goes wrong, it will return the failure status code.
**2.1.2.d** Will return the status code from Cosmos if any error occurs

#### UpdateAccount <a name="updateaccount"></a>
**2.1.3.a** Now we will make the Account Updating function.
**2.1.3.b** This account must include the id and the userName of the account to be updated.
**2.1.3.c** We will also encapsulate this in a try-catch, in case of any errors.
**2.1.3.d** We will Upsert the account, if anything goes wrong, it will return the failure status code.
**2.1.3.e** Will return the status code from Cosmos if any error occurs.

#### DeleteAccount <a name="deleteaccount"></a>
**2.1.4.a** Finally, we will add delete functionality.
**2.1.4.b** To delete a document, we are going to need the id of the document and the username of the account.
**2.1.4.c** Specify the partition key value in the request options.
**2.1.4.d** Then return an OK response code if it succeeds.
**2.1.4.e** Otherwise, return the code from Cosmos.

**2.1.5** Now that we are finished with the logic of the AccountRepository, we can put the definition of the functions in the IAccountRepository.  Head on over there now.

## IAccountRepository.cs Definitions <a name="Iaccountrepo"></a>
**2.1.6.x** Add the same references that you have in the AccountRepository, they will come from the functions below.

**2.1.6.a** Now we will add the functions that we just defined.
**2.1.6.b** The first one was the Get Account, add that here.
**2.1.6.c** The next is the Create Account function.
**2.1.6.d** The next is the Update Account function.
**2.1.6.e** The final is the Delete Account function.

**2.1.7** Now that the functionality is defined, we can head on over to the AccountQuery or RootQuery class (depending on which one you chose to do).

## The Query logic
### (Option Dependent) RootQuery Logic  <a name="rootquerylogic"></a>
**2.2.a.1** It's time to add the injection!  We are going to inject the Repository by its interface.  Basically, it's done this way for testing and to save memory.
**2.2.a.2** Add a reference to the IAccountRepository
**2.2.a.3** Remove the old definition of Account, it is no longer needed.
**2.2.a.4** Now we are going to add an argument that must be passed into the query.
**2.2.a.5** You can also add a default if it is an optional parameter, but in this case, we do not want it to be optional.
**2.2.a.6** Now we can just return the function call to GetAccountBy.

### (Option Dependent) AccountQuery Logic  <a name="accountquerylogic"></a>
**2.2.b.1** It's time to add the injection!  We are going to inject the Repository by its interface.  Basically, it's done this way for testing and to save memory.
**2.2.b.2** Add a reference to the IAccountRepository
**2.2.b.3** Remove the old definition of Account, it is no longer needed.
**2.2.b.4** Now we are going to add an argument that must be passed into the query.
**2.2.b.5** You can also add a default if it is an optional parameter, but in this case, we do not want it to be optional.
**2.2.b.6** Now we can just return the function call to GetAccountBy.


**2.3.1** It is now time to register the repository for dependency injection, head on over to the Startup.cs class again.

### Startup (Pt. 5)  <a name="startup5"></a>
**2.3.2.a** Register the Repository as a transient, it will be a new instance of the class every time a call is made.
**2.3.2.b** Register the repository interface and class.
**2.3.2.c** Now that it is registered, the API Should be at a point to use the GetAccount Function. Not that that is defined, we will start the Mutation end of things (Create, Delete, and Update).  Head on over to the RootMutation class to start.

## RootMutation Logic  <a name="rootmutationlogic"></a>
**2.4.1** Now it is time to write the logic for the mutation end of things.  Because there is three functions that need to be defined, we are going to put it in a separate file called AccountMutation.
**2.4.2** Before we Create that file, we are going to register it to the RootMutation so we do not have to come back to this file
**2.4.3** Now Add that file to the Mutations folder. Head on over to that new file now.

### AccountMutation  <a name="accountmutation"></a>
**2.4.3.a.1** Add inheritance to the ObjectGraphType.
**2.4.3.a.2** Add a reference to the ObjectGraphType.

**2.4.3.b.1** Add a constructor for the AccountMutation and have the IAccountRepository be there.
**2.4.3.b.2** Add a reference to the IAccountRepository.

#### Field Logic  <a name="fieldlogic"></a>

##### Create Function  <a name="create"></a>
**2.4.3.d.1** Now add a field for the create function.
**2.4.3.d.2** Add a reference to the Product Graph type.
**2.4.3.d.3** We have to add a query argument for the account that we will be passing in.  This will be of type AccountInput, which we will create in a second.
**2.4.3.d.4** In the model folder, create another folder called Inputs.  In there add the AccountInput type.
**2.4.3.d.5** Add references to the created Inputs, the Account object, and the System.Net for the status codes.
**2.4.3.d.6** Now that the Input is there, it's as simple as hooking up the AccountRepository Logic.
**2.4.3.d.7** We want to start by getting the account from the arguments.  We then want to send that directly to Cosmos to have the account created.

##### Delete Function  <a name="delete"></a>
**2.4.3.e.1** Now we are going to create a field for the delete function.
**2.4.3.e.2** Add two arguments that will be how we identify which account to delete.  They will correspond to the id and the userName.
**2.4.3.e.3** Now that we have those two arguments, we can send them to the DeleteAccount function.
**2.4.3.e.4** After we get back a status code from Cosmos, we want to either return null, which will symbolize a failure, or return the object that was passed in.
**2.4.3.e.5** Obviously this is not a good way to handle Error reporting.  Personally, I would suggest creating a scalar type.  I'll cover that in a little bonus tips and tricks module (Module 3).

##### Update Function  <a name="update"></a>
**2.4.3.f.1** The final field will be the Update function.
**2.4.3.f.2** We have to add a query argument for the account that we will be passing in.  This will be of type AccountInput.
**2.4.3.f.3** Now that the Input is there, it's as simple as hooking up the AccountRepository Logic.
**2.4.3.f.4** We want to start by getting the account from the arguments.  We then want to send that directly to Cosmos to have the account created.
**2.4.3.f.5** After we get back a status code from Cosmos, we want to either return null, which will symbolize a failure, or return the object that was passed in.

**2.5** Now it's time to register everything that we have just done and that will wrap up this tutorial series!  Head on over to the Startup class one last time.

## Startup... Is it Finally Over?  <a name="startupdone"></a>
**2.5.1.a** Add the AccountMutation, AccountInput, AddressInput, and the NameInput to the services.
**2.5.1.b** Add the reference to those Inputs and you are done!

## It is Finally over.  <a name="over"></a>
So you just made a GraphQL API from scratch!  Pat yourself on the back.  So now you are aware of all the things that go into making an API, the many intricate parts that get this machine running smoothly.  Now that we've covered the basics/advanced topics of creating a GraphQL API, let's discuss some of the little extra things that will make your life easier.
 
# Module 3: Advanced Tips and Tricks  <a name="advanced"></a>
Some of the following are pretty straight forward, while others are going to take some research of your own any questions, feel free to reach out to me.

## Scalar Types  <a name="scalars"></a>
Now Scalar types in GraphQL are any type that can not be queried any lower than the type they are defined.  Take a string for example.  A string is a predefined scalar type that does not have any other levels (fields) that can be queried.  This is the definition of a string type from GraphQL DotNet [Github](https://github.com/graphql-dotnet/graphql-dotnet/blob/master/src/GraphQL/Types/StringGraphType.cs):
```csharp
using GraphQL.Language.AST;

namespace GraphQL.Types
{
    public class StringGraphType : ScalarGraphType
    {
        public StringGraphType() => Name = "String";

        public override object Serialize(object value) => value?.ToString();

        public override object ParseValue(object value) => value?.ToString();

        public override object ParseLiteral(IValue value) => (value as StringValue)?.Value;
    }
}
``` 
Now with looking at how the string implements the scalar type, you can kind of understand what is going on.  The object is being taken in and parsed to a string.  Now using this knowledge, we can create a scalar type.  Let's make a scalar type for a Dictionary.  We will call it DictionaryGraph.  How we do create it is like so:
```csharp
using GraphQL.Language.AST;
using GraphQL.Types;
using System.Collections.Generic;
using GraphQL;

namespace API.Model.Graph.ScalarTypes
{
    public class DictionaryGraphType<T, TT> : ScalarGraphType
    {
        public DictionaryGraphType() => Name = $"Dictionary_{typeof(T).Name}_{typeof(TT).Name}";
        public override object ParseLiteral(IValue value) => ParseValue(value);
        public override object ParseValue(object value) => ValueConverter.ConvertTo(value, typeof(Dictionary<T, TT>));
        public override object Serialize(object value) => ParseValue(value);
    }
}

```
For all of the methods that we are required to inherit, we will convert the object that is passed into a dictionary of the type that it was created with.  You can apply this example to pretty much any scalar type that you could possibly create.

## Error Handling  <a name="errorhandling"></a>
Error handling should be done on several fronts, actual errors, status code, and what caused the error.  To accomplish this you can make a custom response that sends back the data, a status code, and a dictionary of errors and where they occurred.  Alternatively, you can send back a list of status codes that correspond to a list of errors.  There are many different approaches you can take, but this is more of a by need basis.  To actually log the errors to GraphQL, you are going to add them in the context. 
So in a class that is an ObjectGraphType: (AccountQuery, AccountMutation, etc.)
```csharp
using GraphQL;
using GraphQL.Types;
...
resolve: context =>
{
    ... Code above that leads to error
    context.Errors.Add(new ExecutionError("An Error Occurred"));
    ...
}
```
You can also make custom ExecutionErrors, which I did.  This allowed me to log a status code of what the error actually was.  Reporting the errors here is important, as they are bubbled up to the controller. 

## Making Custom Types  <a name="customtypes"></a>
Custom types can be quite beneficial when it comes to doing advanced functionality.  Don't be afraid to inherit some stuff and add other fields onto it.
