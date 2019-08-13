//TODO 3.5.b Add the references to the Account class and the ObjectGraphType.
using FollowAlongLearnAPI.Model.Base;
using GraphQL.Types;

namespace FollowAlongLearnAPI.Model.GraphTypes
{
    //TODO 3.5.a Add inheritance to the ObjectGraphType (translating to GraphQL) and have that have the type of Account.
    public class AccountGraph : ObjectGraphType<Account> 
    {
        //TODO 3.6.a Now create the constructor for the AccountGraph
        public AccountGraph()
        {
            //TODO 3.6.b Add a name and description to the AccountGraph.
            Name = "Account";
            Description = "Holds all the information associated with a given account";

            //TODO 3.7.a Now we are going to define each of the fields on the account type as a GraphQL type.
            Field(account => account.UserName).Name("userName");  //TODO 3.7.a.1 Right here we are saying "GraphQL this field should be exposed for querying (username) and its name should be userName"
            //TODO 3.7.b Now do that for the rest of the fields. NOTE this can only be done for types that are Primitive, if there's a type that is different, like Address, you will have to make a separate Graph Type for it.
            Field(account => account.Password).Name("password");
            //TODO 3.8.a Now create a GraphType for the Name model type.  Put it in the same GraphTypes folder.
            //TODO 3.8.d Now that we created that GraphType, we are going to register it like so:
            Field<NameGraph>().Name("name");  
            //TODO 3.9.a Now create a GraphType for the Address model type.  Put it in the same GraphTypes folder.
            //TODO 3.9.d Now that we created that GraphType, we are going to register it like so:
            Field<AddressGraph>().Name("address"); //TODO 3.9.x / 3.8.x Here we are saying that this type has to be broken down more so go to that file to resolve the type that we pass it.
        }
    }
}

//TODO 4.0 Now we are ready to define the functionality of this new type.  We'll start with the Query portion of this Type, then we will move into the mutation section.  Head on over to the RootQuery class.