// TODO 2.0.b Add a reference to GraphQL.Types to use the ObjectGraphType
using GraphQL.Types;
//TODO 4.1.c.2 Add a reference to the AccountGraph and the Account, Address, and Name types.
using FollowAlongLearnAPI.Model.GraphTypes;
using FollowAlongLearnAPI.Model.Base;

namespace FollowAlongLearnAPI.MiddleWare.Queries
{
    //TODO 1.8.d.1 Make the RootQuery class, we will come back to it after we are finished with the Schema
    
    //TODO 2.0.a Add inheritance to ObjectGraphType, a GraphQL type that allows it to know it belongs to GraphQL 
    public class RootQuery : ObjectGraphType // TODO 2.0.c Now if you look in the schema class, the error that was associated with this class should be gone.  That's because it is now registered as an ObjectGraphType, which it was looking for.
    {
        //TODO 2.1.a Create the constructor for the RootQuery.
        public RootQuery()
        {
            //TODO 2.1.b Now give it a name.  It's always smart to name your queries to the base type that they are defining. IE: we will name this one 'Query'
            Name = "Query";
            //TODO 2.1.c Also getting in the habit of giving each Graph Type a description will help the front end a lot.
            Description = "The highest definition of query";
            //TODO 2.1.d After we create query classes, they will be defined here: We'll come back here after a little while. Now head over to the RootMutation.

            //TODO 4.1.a The first method we can use to defining the queries is to have all of the functionality be here.  Alternatively we can create a separate file, to see this method follow along with the 4.2 path.
            //TODO 4.1.b For this given instance, it is okay to keep all of this functionality in one file because all we have is the AccountGraph.  If your file were to be bigger than just one type, I would advise using the second method. Now we are going to create a field.  This is going to be how we get the actual value of the account, it will come through here.
            Field<AccountGraph>(  //TODO 4.1.c.1 We are going to set this to AccountGraph, this is the return type of the field.
                "account",
                resolve: context =>
                {
                    //TODO 4.1.d For now, we are just going to return a new Account with some default values.  This will be like this until we write the repository logic.
                    return new Account()
                    {
                        Address = new Address()
                        {
                            Country = "USA",
                            BoxNumber = "5432",
                            State = "PA",
                            Street = "YeetStreet",
                            Zipcode = "12345"
                        },
                        Name = new Name()
                        {
                            FirstName = "Bob",
                            LastName = "Smith",
                            MiddleInitial = "G"
                        },
                        UserName = "BSmith123",
                        Password = "$hgoe431nkdf"
                    };
                });

            //TODO 4.2.a Second method, creating another file called AccountQuery.cs.  To tell the RootQuery that there is another Query you create an empty version of the file and set that as the resolver.  This is also the first time that you are seeing the resolver.  The resolver is responsible for determining what file to inject using the IServiceProvider from the Schema.cs file.  It will make more sense as time goes on.
            //TODO 4.2.b When we say new { }, we are essentially adding all of the functionality of AccountQuery to the RootQuery.cs file.  Giving it a field makes it so there is another level of declaration before getting to the base functionality.
            Field<AccountQuery>("accountQuery", resolve: context => new { });  //TODO 4.2.c Now add the AccountQuery file to the queries folder.
        }
    }
}

//TODO 4.3 Now we are going to head to the Startup class again to add all of the stuff we just created.  Then we'll be able to give our API the first run!
