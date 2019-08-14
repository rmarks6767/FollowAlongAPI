//TODO 2.4.3.a.2 Add a reference to the ObjectGraphType.
using GraphQL.Types;
//TODO 2.4.3.b.2 Add a reference to the IAccountRepository.
using FollowAlongLearnAPI.Repositories.Interfaces;
//TODO 2.4.3.d.2 Add a reference to the Product Graph type.
using FollowAlongLearnAPI.Model.GraphTypes;
//TODO 2.4.3.d.5 Add references to the created Inputs, the Account object, and the System.Net for the status codes.
using FollowAlongLearnAPI.Model.Inputs;
using FollowAlongLearnAPI.Model.Base;
using System.Net;

namespace FollowAlongLearnAPI.MiddleWare.Mutations
{
    //TODO 2.4.3.a.1 Add inheritance to the ObjectGraphType.
    public class AccountMutation : ObjectGraphType
    {
        //TODO 2.4.3.b.1 Add a constructor for the AccountMutation and have the IAccountRepository be there.
        public AccountMutation(IAccountRepository accountRepository)
        {
            //TODO 2.4.3.c Give the Mutation a name and description.
            Name = "AccountMutation";
            Description = "Used to Create, Update, or Delete an account";

            //TODO 2.4.3.d.1 Now add a field for the create function.
            Field<AccountGraph>(
                "create",
                arguments: new QueryArguments() //TODO 2.4.3.d.3 We have to add a query argument for the account that we will be passing in.  This will be of type AccountInput, which we will create in a second.
                {
                    new QueryArgument<AccountInput>() //TODO 2.4.3.d.4 In the model folder, create another folder called Inputs.  In there add the AccountInput type.
                    {
                        Name = "account"
                    }
                },
                resolve: context => 
                {
                    //TODO 2.4.3.d.6 Now that the Input is there, it's as simple as hooking up the AccountRepository Logic.

                    //TODO 2.4.3.d.7 We want to start by getting the account from the arguments.  We then want to send that directly to Cosmos to have the account created.
                    Account accountToCreate = context.GetArgument<Account>("account");
                    HttpStatusCode code = accountRepository.CreateAccount(accountToCreate).Result;

                    //TODO 2.4.3.d.8 After we get back a status code from Cosmos, we want to either return null, which will symbolize a failure, or return the object that was passed in.
                    if (code == HttpStatusCode.OK)
                        return accountToCreate;
                    else
                        return null;
                });

            //TODO 2.4.3.e.1 Now we are going to create a field for the delete function.
            Field<AccountGraph>(
                "delete",
                arguments: new QueryArguments() 
                { 
                    //TODO 2.4.3.e.2 Add two arguments that will be how we identify which account to delete.  They will correspond to the id and the userName.
                    new QueryArgument<StringGraphType>() 
                    {
                        Name = "id"
                    },
                    new QueryArgument<StringGraphType>()
                    {
                        Name = "userName"
                    }
                },
                resolve: context =>
                {
                    //TODO 2.4.3.e.3 Now that we have those two arguments, we can send them to the DeleteAccount function.

                    HttpStatusCode code = accountRepository.DeleteAccount(context.GetArgument<string>("id"), context.GetArgument<string>("userName")).Result;

                    //TODO 2.4.3.e.4 After we get back a status code from Cosmos, we want to either return null, which will symbolize a failure, or return the object that was passed in.
                    if (code == HttpStatusCode.OK)
                        return null;
                    else
                        return new Account()  //TODO 2.4.3.e.5 Obviously this is not a good way to handle Error reporting.  Personally I would suggest creating a scalar type.  I'll cover that in a little bonus tips and tricks (Module 3).
                        {
                            UserName = "NOT DELETED",
                            Password = "NOT DELETED",
                            Address = new Address()
                            {
                                BoxNumber = "NOT DELETED",
                                State = "NOT DELETED",
                                Street = "NOT DELETED",
                                Country = "NOT DELETED",
                                Zipcode = "NOT DELETED"
                            },
                            Name = new Name()
                            {
                                FirstName = "NOT DELETED",
                                LastName = "NOT DELETED",
                                MiddleInitial = "NOT DELETED",
                            },
                            Id = "NOT DELETED"
                        };
                });

            //TODO 2.4.3.f.1 The final field will be the Update function.
            Field<AccountGraph>(
                "update",
                arguments: new QueryArguments() //TODO 2.4.3.f.2 We have to add a query argument for the account that we will be passing in.  This will be of type AccountInput.
                {
                    new QueryArgument<AccountInput>() 
                    {
                        Name = "account"
                    }
                },
                resolve: context =>
                {
                    //TODO 2.4.3.f.3 Now that the Input is there, it's as simple as hooking up the AccountRepository Logic.

                    //TODO 2.4.3.f.4 We want to start by getting the account from the arguments.  We then want to send that directly to Cosmos to have the account created.
                    Account accountToUpdate = context.GetArgument<Account>("account");
                    HttpStatusCode code = accountRepository.UpdateAccount(accountToUpdate).Result;

                    //TODO 2.4.3.f.5 After we get back a status code from Cosmos, we want to either return null, which will symbolize a failure, or return the object that was passed in.
                    if (code == HttpStatusCode.OK)
                        return accountToUpdate;
                    else
                        return null;
                });
        }
    }
}

//TODO 2.5 Now it's time to register everything that we have just done and that will wrap up this tutorial series!  Head on over to the Startup class one last time.