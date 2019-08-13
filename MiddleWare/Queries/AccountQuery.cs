//TODO 4.2.d.2 Add a reference to the ObjectGraphType.
using GraphQL.Types;
//TODO 4.2.e.3.b Add a reference to the AccountGraph type.
using FollowAlongLearnAPI.Model.GraphTypes;
//TODO 4.2.e.4.b Add a reference to the Account class.
using FollowAlongLearnAPI.Model.Base;

namespace FollowAlongLearnAPI.MiddleWare.Queries
{
    //TODO 4.2.d.1 Add inheritance to ObjectGraphType to tell GraphQL that it is a GraphQL type
    public class AccountQuery : ObjectGraphType
    {
        //TODO 4.2.e.1 Add the Constructor for the AccountQuery
        public AccountQuery() //TODO 4.2.e.1* NOTE that we will inject the repository at this level
        {
            //TODO 4.2.e.2 Give the Query a Name and Description
            Name = "AccountQuery";
            Description = "The account information can be retrieved here";

            //TODO 4.2.e.3 Now we are going to create a field.  This is going to be how we get the actual value of the account, it will come through here.
            Field<AccountGraph>(  //TODO 4.2.e.3.a We are going to set this to AccountGraph, this is the return type of the field.
                "account",
                resolve: context => 
                {
                    //TODO 4.2.e.4.a For now, we are just going to return a new Account with some default values.  This will be like this until we write the repository logic.
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
        }
    }
}