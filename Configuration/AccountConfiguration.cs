//TODO 2.0.4.b You will need to add the Microsoft.Azure.Documents NuGet Package here if you have not already, then add a reference to it.
using Microsoft.Azure.Documents.Client;
//TODO 2.0.4.d.2 Add a reference to System to use the Uri extension.
using System;

namespace FollowAlongLearnAPI.Configuration
{
    public class AccountConfiguration
    {
        //TODO 2.0.3.a The example database that we are going to use is Cosmos DB.  There is an emulator that you can download that will work perfectly for what we are doing. --> https://aka.ms/cosmosdb-emulator
        public string Uri { get; set; }  //TODO 2.0.3.b The URI of the CosmosDb
        public string Key { get; set; }  //TODO 2.0.3.c The Primary Key for access to the Database.
        public string AccountDB { get; set; } //TODO 2.0.3.d The Name of the Database that the accounts are stored in.  
        public string AccountCollection { get; set; }  //TODO 2.0.3.e The collection that the accounts are stored in

        //TODO 2.0.4.a Now we are going to make a function that will return the Document client whenever we need it.
        public DocumentClient GetDocumentClient()
        {
            //TODO 2.0.4.c Cosmos relies on a connection policy, uri, and a Primary key for access.  We are going to make a new connection policy now.
            ConnectionPolicy policy = new ConnectionPolicy()
            {
                ConnectionMode = ConnectionMode.Gateway,  //TODO 2.0.4.c.1 This allows Cosmos to route traffic to the correct partition (That being defined in your Cosmos DB)
                ConnectionProtocol = Protocol.Https,      //TODO 2.0.4.c.2 This allows the HTTPS protocol to be used to connect with Cosmos
                UseMultipleWriteLocations = false,        //TODO 2.0.4.c.3 You can turn this on if you want the data to be written to multiple locations, rather than just one.
                EnableEndpointDiscovery = true,           //TODO 2.0.4.c.4 Allows the endpoint to be discovered for the Cosmos DB.
                EnableReadRequestsFallback = false,       //TODO 2.0.4.c.5 This allows reading from multiple regions.  We are not writing to multiple regions so this can be false. 
            };
            //TODO 2.0.4.d.1 Now that we have set all of the connection policy fields, we can return a new DocumentClient
            return new DocumentClient(new Uri(Uri), Key, policy);
        }
        //TODO 2.0.5 Now make some helper functions to return different URIs for different documents. (Basically, make it easier to connect to Cosmos)
        public Uri GetDocumentUri(string document) => UriFactory.CreateDocumentUri(AccountDB, AccountCollection, document);
        public Uri GetCollectionUri() => UriFactory.CreateDocumentCollectionUri(AccountDB, AccountCollection);
    }
}

//TODO 2.0.6 Now we are going to set up the JSON file to properly populate the above fields with data.  At this time head over to the appsettings.json file.