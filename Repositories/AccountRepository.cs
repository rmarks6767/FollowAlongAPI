//TODO 2.0.2.b Add a reference to the Interfaces folder.
using FollowAlongLearnAPI.Repositories.Interfaces;
//TODO 2.1.0.b Add a reference to the DocumentClient.
using Microsoft.Azure.Documents.Client;
//TODO 2.1.0.c.2 Add a reference to the IOptions and the Configuration class that we set up.  The IOptions is just there because that it how it will be injected in the configuration that we set up in the Startup class.
using FollowAlongLearnAPI.Configuration;
using Microsoft.Extensions.Options;
//TODO 2.1.1.b Add a reference to the Account type.
using FollowAlongLearnAPI.Model.Base;
//TODO 2.1.1.c.2 Add a reference to IQueryable to use in the function.
using System.Linq;
//TODO 2.1.2.a.2 Add references to the Document and Task for use in the create function.  Also a statuscode to use to see if there was a success or failure.
using Microsoft.Azure.Documents;
using System.Threading.Tasks;
using System.Net;

namespace FollowAlongLearnAPI.Repositories
{
    //TODO 2.0.1.a Now we will create an Interface for the repository so that it can be injected by the interface. Create this file in the Repositories folder in another folder called Interfaces.  Call this file IAccountRepository.
    //TODO 2.0.2.a Now add inheritance to the interface that you just created.
    public class AccountRepository : IAccountRepository
    {
        //TODO 2.0.3 Before we move forward with the AccountRepository we need to make a configuration of the database info.  Create a file called AccountConfiguration.  You can put this in a folder called Configuration.

        //TODO 2.1.0.a.1 Now that the Configuration is all set up, we are ready to move full steam ahead.  Here, we are going to add a document client field that the file can use to establish a connection with Cosmos.
        private readonly DocumentClient client;
        //TODO 2.1.0.a.2 This will give us all of the information that we need to connect with cosmos.  We are going to set it in the constructor.
        private readonly AccountConfiguration dbHandler;
        //TODO 2.1.0.c Now, we are going to set the client in the constructor of the AccountRepository.
        public AccountRepository(IOptions<AccountConfiguration> options) //TODO 2.1.0.c.1 This will take in a version of the Configuration that we set up. 
        {
            //TODO 2.1.0.c.3 We then set the dbhandler equal to the configuration file we set up and get the Document Client from the handler.
            dbHandler = options.Value;
            client = dbHandler.GetDocumentClient();
        }

        //TODO 2.1.1.a Now we will create four functions, create, delete, update, and get.  We will start with the get because that one will be the most straight forward.
        public Account GetAccountBy(string userName)
        {
            //TODO 2.1.1.c Put the entire thing in a try catch because it could fail if Cosmos is not online.
            try
            {
                IQueryable<Account> query = client.CreateDocumentQuery<Account>(  //TODO 2.1.1.d We will now query Cosmos using the CreateDocumentQuery Function.  We specify that it is of type Account, so that it will return an object that is an account.
                dbHandler.GetCollectionUri())
                .Where(account => account.UserName == userName); //TODO 2.1.1.e We then say that the userName (which will be our partition key) must equal the username that we provide. 
                //TODO 2.1.1.f We then return the first result that we get from Cosmos.
                return query.AsEnumerable().FirstOrDefault();
            }
            catch
            {
                //TODO 2.1.1.g If it fails for whatever reason, we will return a default of the Account type.
                return default(Account);
            }
        }

        //TODO 2.1.2.a.1 Now we will make the Account Creation function
        public async Task<HttpStatusCode> CreateAccount(Account accountToCreate)
        {
            //TODO 2.1.2.b We will also encapsulate this in a try catch, in case of any errors.
            try
            {
                //TODO 2.1.2.c We will create the new account, if anything goes wrong, it will return the failure status code.
                _ = await client.CreateDocumentAsync(dbHandler.GetCollectionUri(), accountToCreate);
                return HttpStatusCode.OK;
            }
            catch(DocumentClientException e)
            {
                //TODO 2.1.2.d Will return the status code from Cosmos if any error occurs
                return (HttpStatusCode)e.StatusCode;
            }
        }

        //TODO 2.1.3.a Now we will make the Account Updating function.
        public async Task<HttpStatusCode> UpdateAccount(Account accountToBeUpdated) // TODO 2.1.3.b This account must include the id and the userName of the account to be updated.
        {
            //TODO 2.1.3.c We will also encapsulate this in a try catch, in case of any errors.
            try
            {
                //TODO 2.1.3.d We will Upsert the account, if anything goes wrong, it will return the failure status code.
                ResourceResponse<Document> resource = await client.UpsertDocumentAsync(dbHandler.GetCollectionUri(), accountToBeUpdated, disableAutomaticIdGeneration: true);
                accountToBeUpdated.Id = resource.Resource.Id;
                return HttpStatusCode.OK;
            }
            catch (DocumentClientException e)
            {
                //TODO 2.1.3.e Will return the status code from Cosmos if any error occurs.
                return (HttpStatusCode)e.StatusCode;
            }
        }

        //TODO 2.1.4.a Finally, we will add delete functionality.
        public async Task<HttpStatusCode> DeleteAccount(string id, string userName)
        {
            try
            {
                //TODO 2.1.4.b To delete a document, we are going to need the id of the document and the username of the account.
                _ = await client.DeleteDocumentAsync(dbHandler.GetDocumentUri(id), new RequestOptions()
                {
                    PartitionKey = new PartitionKey(userName) //TODO 2.1.4.c Specify the partition key value in the request options.
                });
                //TODO 2.1.4.d Then return an OK response code if it succeeds.
                return HttpStatusCode.OK;
            }
            catch (DocumentClientException e)
            {
                //TODO 2.1.4.e Otherwise return the code from Cosmos.
                return (HttpStatusCode)e.StatusCode;
            }
        }

        //TODO 2.1.5 Now that we are finished with the logic of the AccountRepository, we can put the definition of the functions in the IAccountRepository.  Head on over there now.

    }
}
