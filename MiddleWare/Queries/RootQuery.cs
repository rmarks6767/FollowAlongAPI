// TODO 2.0.b Add a reference to GraphQL.Types to use the ObjectGraphType
using GraphQL.Types;

namespace FollowAlongLearnAPI.MiddleWare.Queries
{
    //TODO 1.8.d.1 Make the RootQuery class, we will come back to it after we are finished with the Schema
    
    //TODO 2.0.a Add inheritance to ObjectGraphType, a GraphQL type that allows it to know it belongs to GraphQL 
    public class RootQuery : ObjectGraphType // TODO 2.0.c Now if you look in the schema class, the error that was associated with this class should be gone.  That's because it is now registered as an ObjectGraphType, which it was looking for.
    {
        //TODO 2.1.a Create the constructor for the RootQuery
        public RootQuery()
        {
            //TODO 2.1.b Now give it a name.  It's always smart to name your queries to the base type that they are defining. IE: we will name this one 'Query'
            Name = "Query";
            //TODO 2.1.c Also getting in the habit of giving each Graph Type a description will help the front end a lot.
            Description = "The highest definition of query";
            //TODO 2.1.d After we create query classes, they will be defined here: We'll come back here after a little while. Now head over to the RootMutation.
        }
    }
}
