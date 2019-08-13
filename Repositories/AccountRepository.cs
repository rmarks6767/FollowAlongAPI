//TODO 2.0.2.b Add a reference to the Interfaces folder.
using FollowAlongLearnAPI.Repositories.Interfaces;

namespace FollowAlongLearnAPI.Repositories
{
    //TODO 2.0.1.a Now we will create an Interface for the repository so that it can be injected by the interface. Create this file in the Repositories folder in another foldder called Interfaces.  Call this file IAccountRepository.
    //TODO 2.0.2.a Now add inheritance to the interface that you just created.
    public class AccountRepository : IAccountRepository
    {
        //TODO 2.0.3 Before we move forward with the AccountRepository we need to make a configuration of the database info.  Create a file called AccountConfiguration.  You can put this in a folder called Configuration.
    }
}
