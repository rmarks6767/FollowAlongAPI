// TODO 2.3.b Add a reference to GraphQL.Types to use the ObjectGraphType
using GraphQL.Types;

namespace FollowAlongLearnAPI.MiddleWare.Mutations
{
    //TODO 1.8.d.2 Add the RootMutation class to the project, we will come back here after making the RootQuery class

    //TODO 2.3.a Add inheritance to ObjectGraphType, a GraphQL type that allows it to know it belongs to GraphQL 
    public class RootMutation : ObjectGraphType // TODO 2.0.c Now if you look in the schema class again, the error that was associated with this class should be gone.
    {
        //TODO 2.4.a Create the constructor for the RootQuery
        public RootMutation()
        {
            //TODO 2.4.b Now give it a name, for the same reason you gave the mutation a name
            Name = "Mutation";
            //TODO 2.4.c Also getting in the habit of giving each Graph Type a description will help the front end a lot.
            Description = "The highest definition of mutation";
            //TODO 2.4.d The field definitions will go here.  First, let's go back to the Startup class and add the files we just created.
        }
    }
}