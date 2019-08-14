//TODO 1.4.2.d.2 Add a reference to the ObjectGraphType.
using GraphQL.Types;
//TODO 1.4.2.e.3.b Add a reference to the AccountGraph type.
using FollowAlongLearnAPI.Model.GraphTypes;
//TODO 1.4.2.e.4.b Add a reference to the Account class.
using FollowAlongLearnAPI.Model.Base;
//TODO 2.2.b.2 Add a reference to the IAccountRepository
using FollowAlongLearnAPI.Repositories.Interfaces;

namespace FollowAlongLearnAPI.MiddleWare.Queries
{
    //TODO 1.4.2.d.1 Add inheritance to ObjectGraphType to tell GraphQL that it is a GraphQL type
    public class AccountQuery : ObjectGraphType
    {
        //TODO 1.4.2.e.1 Add the Constructor for the AccountQuery
        public AccountQuery(IAccountRepository accountRepository) //TODO 1.4.2.e.1* NOTE that we will inject the repository at this level
        {
            //TODO 2.2.b.1 It's time to add the injection!  We are going to inject the Repository by it's interface.  Basically it's done this way for testing and to save memory.


            //TODO 1.4.2.e.2 Give the Query a Name and Description
            Name = "AccountQuery";
            Description = "The account information can be retrieved here";

            //TODO 1.4.2.e.3 Now we are going to create a field.  This is going to be how we get the actual value of the account, it will come through here.
            Field<AccountGraph>(  //TODO 1.4.2.e.3.a We are going to set this to AccountGraph, this is the return type of the field.
                "account",
                arguments: new QueryArguments()
                {
                    new QueryArgument<StringGraphType>()
                    {
                        Name = "userName"
                        //TODO 2.2.b.5 You can also add a default if it is an optional parameter, but in this case, we do not want it to be optional.
                    }
                },
                resolve: context =>
                {
                    //TODO 2.2.b.3 Remove the old definition of Account, it is no longer needed.
                    //TODO 2.2.b.4 Now we are going to add an argument that must be passed into the query.
                    //TODO 2.2.b.6 Now we can just return the function call to GetAccountBy.
                    return accountRepository.GetAccountBy(context.GetArgument<string>("userName"));

                    //TODO 2.3 Now head back to the RootQuery, the next step will continue there.

                    //TODO 1.4.2.e.4.a For now, we are just going to return a new Account with some default values.  This will be like this until we write the repository logic.
                    /*return new Account()
                    {
                        Address = new Address()
                        {
                            Country = "USA",
                            BoxNumber = "5432",
                            State = "PA",
                            Street = "YeetStreet",    This will be removed in Module 2 so that is why it is commented out.
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
                    };*/
                }); ;
        }
    }
}