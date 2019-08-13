/*Schema Overview 
 The schema is what actually makes up GraphQL.  Defining logic in 'Graphs' allows us
 to connect data without making it all relational. This is the root upon which all
 'endpoints' will be defined.  In a traditional model, this corresponds to different 
 endpoints and controllers you would have to make.  By using GraphQL, you alleviate 
 the need to make a new controller every time you have new data available.  This 
 makes versioning seemingly effortless and allows you to move forward with your API 
 effortlessly.
 */

//TODO 1.1.7.b rename the reference to the GraphQL Schema type (because it has the same name as our class)
using GQLSchema = GraphQL.Types.Schema;
//TODO 1.1.8.c Add a reference to System to use the IServiceProvider
using System;
//TODO 1.1.8.e.2 Add These namespaces to use the query, mutation, and the GetRequiredService Extension
using FollowAlongLearnAPI.MiddleWare.Queries;
using FollowAlongLearnAPI.MiddleWare.Mutations;
using Microsoft.Extensions.DependencyInjection;

namespace FollowAlongLearnAPI.MiddleWare
{
    //TODO 1.1.7.a First, add inheritance to the GraphQL.Types.Schema, we can do it by adding a reference to the schema under a different name using GQLSchema = GraphQL.Types.Schema to the namespace
    public class Schema : GQLSchema
    {
        //TODO 1.1.8.a Now make a constructor for our new Schema type
        public Schema(IServiceProvider serviceProvider) : base(serviceProvider) //TODO 1.1.8.b To resolve the types, GraphQL uses IServiceProvider (This comes from all the types that are injected in the Startup class)
        {
            //TODO 1.1.8.d This is where we will add a reference to the RootQuery and RootMutation.  Make those files now in the middleware folder.  These should be in their own folders called Queries and Mutations Respectively.
            //TODO 1.1.8.e.1 Now we can add a reference to both of those classes.  You should get errors because we have not set up either of those classes yet.
            Query = serviceProvider.GetRequiredService<RootQuery>();
            //Mutation = serviceProvider.GetRequiredService<RootMutation>();
        }
    }
}

//TODO 1.1.9 The Schema is now all done so let's head over to the RootQuery class and set that one up.