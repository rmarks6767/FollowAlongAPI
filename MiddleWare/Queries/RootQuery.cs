//TODO 1.2.0.b Add a reference to GraphQL.Types to use the ObjectGraphType
using GraphQL.Types;
//TODO 1.4.1.c.2 Add a reference to the AccountGraph and the Account, Address, and Name types.
using FollowAlongLearnAPI.Model.GraphTypes;
using FollowAlongLearnAPI.Model.Base;
//TODO 2.2.a.2 Add a reference to the IAccountRepository
using FollowAlongLearnAPI.Repositories.Interfaces;

namespace FollowAlongLearnAPI.MiddleWare.Queries
{
    //TODO 1.1.8.d.1 Make the RootQuery class, we will come back to it after we are finished with the Schema

    //TODO 1.2.0.a Add inheritance to ObjectGraphType, a GraphQL type that allows it to know it belongs to GraphQL 
    public class RootQuery : ObjectGraphType // TODO 1.2.0.c Now if you look in the schema class, the error that was associated with this class should be gone.  That's because it is now registered as an ObjectGraphType, which it was looking for.
    {
        //TODO 1.2.1.a Create the constructor for the RootQuery.
        public RootQuery(IAccountRepository accountRepository)
        {
            //TODO 2.2.a.1 It's time to add the injection!  We are going to inject the Repository by its interface.  Basically, it's done this way for testing and to save memory.


            //TODO 1.2.1.b Now give it a name.  It's always smart to name your queries to the base type that they are defining. IE: we will name this one 'Query'
            Name = "Query";
            //TODO 1.2.1.c Also getting in the habit of giving each Graph Type a description will help the front end a lot.
            Description = "The highest definition of query";
            //TODO 1.2.1.d After we create query classes, they will be defined here: We'll come back here after a little while. Now head over to the RootMutation.

            //TODO 1.4.1.a The first method we can use to defining the queries is to have all of the functionality be here.  Alternatively we can create a separate file, to see this method follow along with the 4.2 path.
            //TODO 1.4.1.b For this given instance, it is okay to keep all of this functionality in one file because all we have is the AccountGraph.  If your file were to be bigger than just one type, I would advise using the second method. Now we are going to create a field.  This is going to be how we get the actual value of the account, it will come through here.
            Field<AccountGraph>(  //TODO 1.4.1.c.1 We are going to set this to AccountGraph, this is the return type of the field.
                "account",
                arguments: new QueryArguments()
                {
                    new QueryArgument<StringGraphType>()
                    {
                        Name = "userName"
                        //TODO 2.2.a.5 You can also add a default if it is an optional parameter, but in this case, we do not want it to be optional.
                    }
                },
                resolve: context =>
                {
                    //TODO 2.2.a.3 Remove the old definition of Account, it is no longer needed.

                    //TODO 1.4.1.d For now, we are just going to return a new Account with some default values.  This will be like this until we write the repository logic.
                    //Used to be: return new Account()...
                    /*return new Account(); This will be removed in module 2*/

                    //TODO 2.2.a.4 Now we are going to add an argument that must be passed into the query.
                    //TODO 2.2.a.6 Now we can just return the function call to GetAccountBy.
                    return accountRepository.GetAccountBy(context.GetArgument<string>("userName"));
                });

            //TODO 1.4.2.a Second method, creating another file called AccountQuery.cs.  To tell the RootQuery that there is another Query you create an empty version of the file and set that as the resolver.  This is also the first time that you are seeing the resolver.  The resolver is responsible for determining what file to inject using the IServiceProvider from the Schema.cs file.  It will make more sense as time goes on.
            //TODO 1.4.2.b When we say new { }, we are essentially adding all of the functionality of AccountQuery to the RootQuery.cs file.  Giving it a field makes it so there is another level of declaration before getting to the base functionality.
            Field<AccountQuery>("accountQuery", resolve: context => new { });  //TODO 1.4.2.c Now add the AccountQuery file to the queries folder.
        }
    }
}

//TODO 1.4.3 Now we are going to head to the Startup class again to add all of the stuff we just created.  Then we'll be able to give our API the first run!

//TODO 2.3.1 It is now time to register the repository for dependency injection, head on over to the Startup.cs class again.