//TODO 2.1.6.x Add the same references that you have in the AccountRepository, they will come from the functions below.
using FollowAlongLearnAPI.Model.Base;
using System.Net;
using System.Threading.Tasks;

namespace FollowAlongLearnAPI.Repositories.Interfaces
{
    //TODO 2.0.1.b We will fill this once we create the methods in the AccountRepository class.
    public interface IAccountRepository
    {
        //TODO 2.1.6.a Now we will add the functions that we just defined.

        //TODO 2.1.6.b The first one was the Get Account, add that here.
        Account GetAccountBy(string userName);
        //TODO 2.1.6.c The next is the Create Account function.
        Task<HttpStatusCode> CreateAccount(Account accountToCreate);
        //TODO 2.1.6.d The next is the Update Account function.
        Task<HttpStatusCode> UpdateAccount(Account accountToBeUpdated);
        //TODO 2.1.6.e The final is the Delete Account function.
        Task<HttpStatusCode> DeleteAccount(string id, string userName);
    }
}

//TODO 2.1.7 Now that the functionality is defined, we can head on over to the AccountQuery or RootQuery class (depending on which one you chose to do).